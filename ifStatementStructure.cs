using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyLanguageCompilerProject;

namespace TinyLanguageComilerProject
{
    partial class Parser
    {
        bool LookaheadIf()
        {
            return (StreamIndex < TokenStream.Count && TokenStream[StreamIndex].TokenType == TokenClass.ReservedWordIf);
        }
        private Node? IfStatement()
        {
            Node ifKeyword = new Node("If");
            Node next = ifKeyword;
            do
            {
                Node? matchedIf = match(next == ifKeyword ? TokenClass.ReservedWordIf : TokenClass.ReservedWordElseIf, next);
                Node? conditions = Conditions();
                Node? matchedThen = match(TokenClass.ReservedWordThen, next);
                Node? statements = Statements(ifElseIfBreaker);
                if (matchedIf == null || conditions == null ||
                    matchedThen == null || statements == null)
                {
                    return null;
                }
                next.Children.Add(matchedIf);
                next.Children.Add(conditions);
                next.Children.Add(matchedThen);
                next.Children.Add(statements);
                if (StreamIndex >= TokenStream.Count ||
                    TokenStream[StreamIndex].TokenType != TokenClass.ReservedWordElseIf)
                {
                    break;
                }
                next = new Node("ElseIf");
                ifKeyword.Children.Add(next);
            } while (true);
            ElseStatement(ifKeyword);
            if (ifKeyword.Children.Last().Name != "end")
            {
                return null;
            }
            return ifKeyword;
        }
        private void ElseStatement(Node ifStatement)
        {
            if (StreamIndex >= TokenStream.Count)
            {
                return;
            }
            TokenClass tokenClass = TokenStream[StreamIndex].TokenType;
            if (tokenClass == TokenClass.ReservedWordElse)
            {
                Node elseStatement = new Node("Else");
                elseStatement.Children.Add(match(tokenClass, elseStatement));
                Node? statements = Statements(elseBreaker);
                if (statements == null)
                {
                    return;
                }
                elseStatement.Children.Add(statements);
                ifStatement.Children.Add(elseStatement);
            }
            Node? matchedEnd = match(TokenClass.ReservedWordEnd, ifStatement);
            if (matchedEnd != null)
            {
                ifStatement.Children.Add(matchedEnd);
            }
        }
        private Node? Conditions()
        {
            Node conditions = new Node("Conditions");
            Node? condition = Condition();
            if (condition == null)
            {
                return null;
            }
            conditions.Children.Add(condition);
            while (StreamIndex < TokenStream.Count)
            {
                Node? logicalOperator = LogicalOperator();
                if (logicalOperator == null)
                {
                    break;
                }
                conditions.Children.Add(logicalOperator);
                condition = Condition();
                if (condition == null)
                {
                    return null;
                }
                conditions.Children.Add(condition);
            }
            return conditions;
        }
        private Node? LogicalOperator()
        {
            Node logicalOperator = new Node("LogicalOperator");
            TokenClass tokenClass = TokenStream[StreamIndex].TokenType;
            if (tokenClass != TokenClass.AndOp && tokenClass != TokenClass.OrOp)
            {
                return null;
            }
            logicalOperator.Children.Add(match(tokenClass, logicalOperator));
            return logicalOperator;
        }
        private Node? Condition()
        {
            Node condition = new Node("Condition");
            Node? identifier = Identifier();
            Node? conditionOperator = ConditionOperator();
            Node? term = Term();
            if (conditionOperator == null ||
                identifier == null || term == null)
            {
                return null;
            }
            condition.Children.Add(identifier);
            condition.Children.Add(conditionOperator);
            condition.Children.Add(term);
            return condition;
        }

        private Node? ConditionOperator()
        {
            Node conditionOperator = new Node("ConditionOperator");
            TokenClass? tokenClass = LookaheadConditionOperator();
            if (tokenClass == null)
            {
                ErrorsList.Add("Expected condition operator.");
                return null;
            }
            conditionOperator.Children.Add(match((TokenClass)tokenClass, conditionOperator));
            return conditionOperator;
        }
        private TokenClass? LookaheadConditionOperator()
        {
            if (StreamIndex >= TokenStream.Count)
            {
                return null;
            }
            switch (TokenStream[StreamIndex].TokenType)
            {
                case TokenClass.GreaterThanOp:
                case TokenClass.LessThanOp:
                case TokenClass.Equal:
                case TokenClass.NotEqualOp:
                    return TokenStream[StreamIndex].TokenType;
            }
            return null;
        }
    }
}
