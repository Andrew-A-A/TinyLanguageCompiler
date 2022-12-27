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
        private Node? Equation()
        {
            if (StreamIndex >= TokenStream.Count)
            {
                return null;
            }
            Node equation = new Node("Equation");
            // Parentheses
            if (TokenStream[StreamIndex].TokenType == TokenClass.LeftParentheses)
            {
                equation.Children.Add(match(TokenClass.LeftParentheses, equation));
                Node? subEquation = Equation();
                if (subEquation == null)
                {
                    return null;
                }
                equation.Children.AddRange(subEquation.Children);
                equation.Children.Add(match(TokenClass.RightParentheses, equation));
                if (!Term_after_operator(equation))
                {
                    return null;
                }
            }
            else
            {
                if (!Terms_before_operator_or_ends_term(equation))
                {
                    return null;
                }
            }
            return equation;
        }
        private bool Terms_before_operator_or_ends_term(Node equation)
        {
            while (true)
            {
                Node? term = Term();
                if (term == null)
                {
                    return false;
                }
                equation.Children.Add(term);
                if (StreamIndex >= TokenStream.Count)
                {
                    return false;
                }
                TokenClass? matchedToken = MatchOperatorIfPossible(equation);
                if (matchedToken == null)
                {
                    break;
                }
                if (StreamIndex >= TokenStream.Count)
                {
                    return false;
                }
                if (TokenStream[StreamIndex].TokenType == TokenClass.LeftParentheses)
                {
                    Node? subEquation = Equation();
                    if (subEquation == null)
                    {
                        return false;
                    }
                    equation.Children.AddRange(subEquation.Children);
                    break;
                }
            }
            return true;
        }
        bool Term_after_operator(Node equation)
        {
            while (true)
            {
                if (StreamIndex >= TokenStream.Count)
                {
                    return false;
                }
                TokenClass? matchedToken = MatchOperatorIfPossible(equation);
                if (matchedToken == null)
                {
                    break;
                }
                if (StreamIndex >= TokenStream.Count)
                {
                    return false;
                }
                if (TokenStream[StreamIndex].TokenType == TokenClass.LeftParentheses)
                {
                    Node? subEquation = Equation();
                    if (subEquation == null)
                    {
                        return false;
                    }
                    equation.Children.AddRange(subEquation.Children);
                    break;
                }
                Node? term = Term();
                if (term == null)
                {
                    return false;
                }
                equation.Children.Add(term);
            }
            return true;
        }
        TokenClass? MatchOperatorIfPossible(Node equation)
        {
            if (TokenStream[StreamIndex].TokenType == TokenClass.PlusOp)
            {
                equation.Children.Add(match(TokenClass.PlusOp, equation));
                return TokenClass.PlusOp;
            }
            if (TokenStream[StreamIndex].TokenType == TokenClass.MinusOp)
            {
                equation.Children.Add(match(TokenClass.MinusOp, equation));
                return TokenClass.MinusOp;
            }
            if (TokenStream[StreamIndex].TokenType == TokenClass.MultiplyOp)
            {
                equation.Children.Add(match(TokenClass.MultiplyOp, equation));
                return TokenClass.MultiplyOp;
            }
            if (TokenStream[StreamIndex].TokenType == TokenClass.DivideOp)
            {
                equation.Children.Add(match(TokenClass.DivideOp, equation));
                return TokenClass.DivideOp;
            }
            return null;
        }
    }
}
