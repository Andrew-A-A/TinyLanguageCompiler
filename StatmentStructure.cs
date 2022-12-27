using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyLanguageCompilerProject;

namespace TinyLanguageComilerProject
{
     partial class Parser {
        bool LookaheadFunctionCall() {
            return (StreamIndex + 1 < TokenStream.Count &&
                TokenStream[StreamIndex].TokenType == TokenClass.Identifier &&
                TokenStream[StreamIndex + 1].TokenType == TokenClass.LeftParentheses);
        }
        Node? FunctionCall() {
            Node functionCall = new Node("FunctionCall");
            Node? identifier = Identifier();
            Node? lParent = match(TokenClass.LeftParentheses, functionCall);
            if (identifier == null || lParent == null) {
                return null;
            }
            functionCall.Children.Add(identifier);
            functionCall.Children.Add(lParent);
            if (StreamIndex < TokenStream.Count &&
                TokenStream[StreamIndex].TokenType != TokenClass.RightParentheses) {
                while (true) {
                    Node? argument = Identifier();
                    if (argument == null) {
                        return null;
                    }
                    functionCall.Children.Add(argument);
                    if (StreamIndex < TokenStream.Count &&
                        TokenStream[StreamIndex].TokenType != TokenClass.Comma) {
                        break;
                    }
                    Node? comma = match(TokenClass.Comma, functionCall);
                    functionCall.Children.Add(comma);
                }
            }
            Node? rParent = match(TokenClass.RightParentheses, functionCall);
            if (rParent == null) {
                return null;
            }
            functionCall.Children.Add(rParent);
            return functionCall;
        }
        bool LookaheadReadStatement() {
            return (StreamIndex < TokenStream.Count && TokenStream[StreamIndex].TokenType == TokenClass.ReservedWordRead);
        }
        Node? ReadStatement() {
            Node read = new Node("Read");
            Node? matchedRead = match(TokenClass.ReservedWordRead, read);
            Node? identifier = Identifier();
            Node? matchSemicolon = match(TokenClass.Semicolon, read);
            if (matchedRead == null || identifier == null || matchSemicolon == null) {
                return null;
            }
            read.Children.Add(matchedRead);
            read.Children.Add(identifier);
            read.Children.Add(matchSemicolon);
            return read;
        }
        bool LookaheadWriteStatement() {
            return (StreamIndex < TokenStream.Count && TokenStream[StreamIndex].TokenType == TokenClass.ReservedWordWrite);
        }
        Node? WriteStatement() {
            Node write = new Node("Write");
            Node? matchedWrite = match(TokenClass.ReservedWordWrite, write);
            Node? writable;
            if (StreamIndex < TokenStream.Count && TokenStream[StreamIndex].TokenType == TokenClass.ReservedWordEndl) {
                writable = match(TokenClass.ReservedWordEndl, write);
            } else {
                writable = Expression();
            }
            Node? matchSemicolon = match(TokenClass.Semicolon, write);
            if (matchedWrite == null || writable == null || matchSemicolon == null) {
                return null;
            }
            write.Children.Add(matchedWrite);
            write.Children.Add(writable);
            write.Children.Add(matchSemicolon);
            return write;
        }
        bool LookaheadReturn() {
            return (StreamIndex < TokenStream.Count && TokenStream[StreamIndex].TokenType == TokenClass.ReservedWordReturn);
        }
        Node? ReturnStatement() {
            Node? ret = new Node("Return");
            Node? matchedReturn = match(TokenClass.ReservedWordReturn, ret);
            Node? expression = Expression();
            Node? matchSemicolon = match(TokenClass.Semicolon, ret);
            if (matchedReturn == null || expression == null || matchSemicolon == null) {
                return null;
            }
            ret.Children.Add(matchedReturn);
            ret.Children.Add(expression);
            ret.Children.Add(matchSemicolon);
            return ret;
        }
        private Node? Statements(List<TokenClass> tokens) {
            Node statements = new Node("Statements");
            while (StreamIndex < TokenStream.Count &&
                !tokens.Contains(TokenStream[StreamIndex].TokenType)) {
                int i = 0;
                for (; i < lookaheads.Length; ++i) {
                    if (lookaheads[i]()) { break; }
                }
                if (i == lookaheads.Length) {
                    return null;
                }
                Node? node = lookedStatements[i]();
                if (node == null) {
                    return null;
                }
                if (node.Name == "AssignmentStatement" || node.Name == "FunctionCall") {
                    Node? semicolon = match(TokenClass.Semicolon, node);
                    if (semicolon == null) { return null; }
                    node.Children.Add(semicolon);
                }
                statements.Children.Add(node);
            }
            return statements;
        }
    }
}
