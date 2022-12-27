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
        private Node? Term()
        {
            //Node term = new Node("Identifier");
            //term.Children.Add(match(Token_Class.Identifier, term));
            //return term;
            // TODO:
            if (StreamIndex >= TokenStream.Count)
            {
                return null;
            }
            Node? term = new Node("Term");
            if (TokenStream[StreamIndex].TokenType == TokenClass.Number)
            {
                Node number = new Node("Number");
                number.Children.Add(match(TokenClass.Number, term));
                term.Children.Add(number);
            }
            else if (LookaheadFunctionCall())
            {
                Node? functionCall = FunctionCall();
                if (functionCall == null)
                {
                    return null;
                }
                term.Children.Add(functionCall);
            }
            else if (TokenStream[StreamIndex].TokenType == TokenClass.Identifier)
            {
                term.Children.Add(Identifier());
            }
            else
            {
                return null;
            }
            return term;
        }
        private Node? DeclarationStatements()
        {
            Node? declarationStatement = new Node("DeclarationStatement");
            Node? datatype = DataType();
            if (datatype == null) { return null; }
            declarationStatement.Children.Add(datatype);
            while (true)
            {
                if (StreamIndex >= TokenStream.Count)
                {
                    return null;
                }
                if (LookaheadAssignmentStatement())
                {
                    Node? assignmentStatement = AssignmentStatement();
                    if (assignmentStatement == null)
                    {
                        return null;
                    }
                    declarationStatement.Children.Add(assignmentStatement);
                }
                else if (TokenStream[StreamIndex].TokenType == TokenClass.Identifier)
                {
                    Node? identifier = Identifier();
                    if (identifier == null)
                    {
                        return null;
                    }
                    declarationStatement.Children.Add(identifier);
                }
                else
                {
                    return null;
                }
                if (StreamIndex >= TokenStream.Count)
                {
                    return null;
                }
                if (TokenStream[StreamIndex].TokenType != TokenClass.Comma)
                {
                    break;
                }
                declarationStatement.Children.Add(match(TokenClass.Comma, declarationStatement));
            }
            Node? semiColon = match(TokenClass.Semicolon, declarationStatement);
            if (semiColon == null) { return null; }
            declarationStatement.Children.Add(semiColon);
            return declarationStatement;
        }
        private bool LookaheadAssignmentStatement()
        {
            return (StreamIndex + 1 < TokenStream.Count &&
                TokenStream[StreamIndex].TokenType == TokenClass.Identifier &&
                TokenStream[StreamIndex + 1].TokenType == TokenClass.AssignOp);
        }
        private Node? AssignmentStatement()
        {
            Node? assignmentStatement = new Node("AssignmentStatement");
            Node? identifier = Identifier();
            Node? assignmentOperator = match(TokenClass.AssignOp, assignmentStatement);
            Node? expression = Expression();
            if (identifier == null || assignmentOperator == null || expression == null)
            {
                return null;
            }
            assignmentStatement.Children.Add(identifier);
            assignmentStatement.Children.Add(assignmentOperator);
            assignmentStatement.Children.Add(expression);
            return assignmentStatement;
        }

        private Node? Expression()
        {
            if (StreamIndex >= TokenStream.Count)
            {
                return null;
            }
            Node expression = new Node("Expression");
            if (TokenStream[StreamIndex].TokenType == TokenClass.StringLiteral)
            {
                Node stringLiteral = new Node("StringLiteral");
                stringLiteral.Children.Add(match(TokenClass.StringLiteral, stringLiteral));
                expression.Children.Add(stringLiteral);
            }
            else
            {
                Node? equation = Equation();
                if (equation == null)
                {
                    return null;
                }
                if (equation.Children.Count == 1)
                {
                    expression.Children.Add(equation.Children[0]);
                }
                else
                {
                    expression.Children.Add(equation);
                }
            }
            return expression;
        }
        private Node? Identifier(Node? parent = null)
        {
            Node identifier = new Node("Identifier");
            Node? identifierName = match(TokenClass.Identifier, parent == null ? identifier : parent);
            if (identifierName == null)
            {
                return null;
            }
            identifier.Children.Add(identifierName);
            return identifier;
        }
        private Node? DataType()
        {
            if (StreamIndex >= TokenStream.Count)
            {
                return null;
            }
            Node? datatype = new Node("DataType");
            Node? type = null;
            if (TokenStream[StreamIndex].TokenType == TokenClass.DataTypeInt)
            {
                type = match(TokenClass.DataTypeInt, datatype);
            }
            else if (TokenStream[StreamIndex].TokenType == TokenClass.DataTypeString)
            {
                type = match(TokenClass.DataTypeString, datatype);
            }
            else if (TokenStream[StreamIndex].TokenType == TokenClass.DataTypeFloat)
            {
                type = match(TokenClass.DataTypeFloat, datatype);
            }
            if (type == null)
            {
                return null;
            }
            datatype.Children.Add(type);
            return datatype;
        }
        private bool LookaheadDatatype()
        {
            if (StreamIndex >= TokenStream.Count)
            {
                return false;
            }
            switch (TokenStream[StreamIndex].TokenType)
            {
                case TokenClass.DataTypeInt:
                case TokenClass.DataTypeString:
                case TokenClass.DataTypeFloat:
                    return true;
            }
            return false;
        }
    }
}
