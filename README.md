# Pathfinding

This interactive demonstration showcases the performance and characteristics of five popular pathfinding algorithms: Depth First Search (DFS), Breadth First Search (BFS), Best First Search, Dijkstra's Algorithm, and A*.
Grid Configuration:
- Walls: Non-walkable cells that act as obstacles. (Black)
- Ground: Walkable cells with a low cost. (White)
- Water: Walkable cells with a higher traversal cost. (Blue)
- Visited neighbours: Walkable cells where the alorithim searched but did nor find a path. (Gray)

Observe the path found by each algorithm, marked with a bright colour for clarity.

## Depth First Search (DFS):
Explores paths deeply before backtracking, tends to follow one path to its end before trying another. 
Not guaranteed to find the shortest path.

<img width="780" alt="Depth" src="https://github.com/user-attachments/assets/e979307b-aea8-41e2-bb4d-f2c1bf250a13">


## Breadth First Search (BFS): 
Explores all neighbors at the current depth level before moving deeper, guarantees finding the shortest path in an unweighted grid. 
Efficient for finding the shortest path in mazes without varying costs.
<img width="780" alt="Breadth" src="https://github.com/user-attachments/assets/c6c7785d-2361-4374-9e71-b55e259414c9">


## Best First Search:
Uses a heuristic to prioritize which nodes to explore, quickly moves towards the goal based on the heuristic estimate. 
Does not guarantee the shortest path in weighted grids.
<img width="780" alt="Best" src="https://github.com/user-attachments/assets/b1113fd0-cb32-40b2-ac69-5a8fc19a87c5">

## Dijkstra's Algorithm:
Explores all possible paths to ensure the shortest path is found, takes varying costs into account, making it suitable for weighted grids.
Guaranteed to find the shortest path.
<img width="780" alt="Dijkstra" src="https://github.com/user-attachments/assets/e1eeaa8e-85c0-444f-b593-2309e505dc64">


## A*:
Combines the actual cost to reach a node and a heuristic estimate to the goal, efficiently finds the shortest path by balancing exploration and goal-directed search.
Guaranteed to find the shortest path with an admissible heuristic.
<img width="780" alt="A*" src="https://github.com/user-attachments/assets/cb2b4598-c2eb-41d0-b573-a005b4f94d6b">


mandelcohen, 2024

