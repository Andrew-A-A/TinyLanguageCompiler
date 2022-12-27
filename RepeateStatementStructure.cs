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
        bool LookaheadRepeat()
        {
            return StreamIndex < TokenStream.Count && TokenStream[StreamIndex].TokenType == TokenClass.ReservedWordRepeat;
        }
        Node? RepeatStatement()
        {
            Node repeat = new Node("Repeat");
            Node? matchedRepeat = match(TokenClass.ReservedWordRepeat, repeat);
            Node? statements = Statements(repeatBreaker);
            Node? matchedUntil = match(TokenClass.ReservedWordUntil, repeat);
            Node? condition = Conditions();
            if (matchedRepeat == null || statements == null ||
                matchedUntil == null || condition == null)
            {
                return null;
            }
            repeat.Children.Add(matchedRepeat);
            repeat.Children.Add(statements);
            repeat.Children.Add(matchedUntil);
            repeat.Children.Add(condition);
            return repeat;
        }
    }
}
