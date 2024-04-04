namespace GraphTheory
{
    class Validation
    {
        // Kiểm tra đồ thị có cạnh bội hay không
        public static bool IsMultiGraph(AdjacencyList adjacencyList)
        {
            for (int i = 0; i < adjacencyList.VertexCount; i++)
            {
                var edges = adjacencyList.GetEdges(i);

                // Kiểm tra từng đỉnh cuối trong list các cạnh xem có bị lặp lại hay không
                for (int j = 0; j < edges.Count; j++)
                {
                    for (int k = j + 1; k < edges.Count; k++)
                    {
                        if (edges[j].Destination == edges[k].Destination) return true;
                    }
                }
            }

            return false;
        }

        public static bool IsUndirectedGraph(AdjacencyMatrix adjacencyMatrix)
        {
            int i, j;
            bool isSymmetric = true;

            for (i = 0; i < adjacencyMatrix.VertexCount && isSymmetric; i++)
            {
                for (j = i + 1; (j < adjacencyMatrix.VertexCount) && (adjacencyMatrix.Data[i, j] == adjacencyMatrix.Data[j, i]); j++) ;
                if (j < adjacencyMatrix.VertexCount)
                    isSymmetric = false;
            }
            return isSymmetric;
        }

        public static bool IsGraphHasLoops(AdjacencyMatrix adjacencyMatrix)
        {
            for (int i = 0; i < adjacencyMatrix.VertexCount && adjacencyMatrix.Data[i, i] == 0; i++)
                if (i < adjacencyMatrix.VertexCount)
                    return true;
            return false;
        }

        //Chuyển ma trận kề có hướng thành ma trận kề vô hướng 
        public static void TranslateGraph(AdjacencyMatrix adjacencyMatrix, int[,] UndirectedGraph)
        {
            int N_adjacencyMatrix = adjacencyMatrix.VertexCount;

            for (int i = 0; i < N_adjacencyMatrix; i++)
            {
                for (int j = 0; j < N_adjacencyMatrix; j++)
                {
                    if (adjacencyMatrix.Data[i, j] != 0)
                    {
                        UndirectedGraph[i, j] = adjacencyMatrix.Data[i, j];
                        UndirectedGraph[j, i] = adjacencyMatrix.Data[i, j];
                    }
                }
            }
        }

        //Kiểm tra đồ thị liên thông hay không liên thông
        public static bool Connected(AdjacencyMatrix adjacencyMatrix)
        {
            int N_adjacencyMatrix = adjacencyMatrix.VertexCount;
            int[,] UndirectedGraph = new int[N_adjacencyMatrix, N_adjacencyMatrix];
            bool[] marked = new bool[adjacencyMatrix.VertexCount];
            
            int Dem = 0;

            //Chuyển đồ thị vô hướng thành đồ thị có hướng
            TranslateGraph(adjacencyMatrix, UndirectedGraph);

            //Khởi tạo mọi đỉnh chưa đánh dấu
            for (int i = 0; i < UndirectedGraph.Length; i++)
            {
                marked[i] = false;
                marked[0] = true;
            }

            bool connect = true;
            do
            {
                connect = true;
                for (int i = 0; i < UndirectedGraph.Length; i++)
                {
                    if (marked[i] == true)
                    {
                        for (int j = 0; j < UndirectedGraph.Length; j++)
                        {
                            if (marked[j] == false && UndirectedGraph[i, j] > 0)
                            {
                                marked[j] = true;
                                connect = true;
                                Dem++;
                                if (Dem == adjacencyMatrix.VertexCount)
                                    return true;
                            }
                        }
                    }
                }
            }
            while (connect == false);
            return false;
        }
    }
}