using System;
using System.Collections.Generic;

namespace Satisfiability_of_Equality_Equations
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] equations = new string[5] { "b==c", "a==b" , "d==e", "a!=e", "a!=c"};
      Solution s = new Solution();
      var answer = s.EquationsPossible(equations);
      Console.WriteLine(answer);
    }
  }

  public class Solution
  {
    // We have kept the same color for the matched equations
    // e.g - a==b, b==c, here colors array will have same color for a,b,c(0,1,2 index), when colors are same for the variables which also says if we visualize it as graph
    // then there exist a path between these variables
    // after updating the color array, will be iterating the equations array for the != equations, during this time if we find a equation say not equals
    // e.g - a!=c, from a==b and b==c say a==c as well, but we have a equation a!=c, so we can not satisfy the equality equation here
    public bool EquationsPossible(string[] equations)
    {
      List<int>[] graph = new List<int>[26];
      for (int i = 0; i < 26; ++i)
        graph[i] = new List<int>();

      foreach (var eqn in equations)
      {
        if (eqn[1] == '=')
        {
          int x = eqn[0] - 'a';
          int y = eqn[3] - 'a';
          graph[x].Add(y);
          graph[y].Add(x);
        }
      }

      int[] color = new int[26];
      Array.Fill(color, -1);

      for (int i = 0; i < 26; i++)
      {
        if (color[i] == -1)
        {
          Dfs(i, i, color, graph);
        }
      }

      foreach (var eqn in equations)
      {
        if (eqn[1] == '!')
        {
          int x = eqn[0] - 'a';
          int y = eqn[3] - 'a';
          if (color[x] == color[y])
            return false;
        }
      }

      return true;
    }

    private void Dfs(int node, int c, int[] color, List<int>[] graph)
    {
      if (color[node] == -1)
      {
        color[node] = c;
        foreach (int nei in graph[node])
          Dfs(nei, c, color, graph);
      }
    }
  }
}
