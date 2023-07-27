class Node {
    int x; // x-coordinate
    int y; // y-coordinate
    boolean visited;
    double gScore; 
    double fScore; 
    List<Node> neighbors; 
}

List<Node> aStar(Node start, Node goal) {
    PriorityQueue<Node> openSet = new PriorityQueue<>((n1, n2) -> Double.compare(n1.fScore, n2.fScore)); 
    Set<Node> closedSet = new HashSet<>(); 
    Map<Node, Node> cameFrom = new HashMap<>(); 
    start.gScore = 0;
    start.fScore = heuristic(start, goal);
    openSet.add(start);

    while (!openSet.isEmpty()) {
        Node current = openSet.poll();
        if (current == goal) {
            return reconstructPath(cameFrom, current);
        }
        closedSet.add(current);
        for (Node neighbor : current.neighbors) {
            if (closedSet.contains(neighbor)) {
                continue;
            }
            double tentativeGScore = current.gScore + distance(current, neighbor);
            if (!openSet.contains(neighbor) || tentativeGScore < neighbor.gScore) {
                cameFrom.put(neighbor, current);
                neighbor.gScore = tentativeGScore;
                neighbor.fScore = neighbor.gScore + heuristic(neighbor, goal);
                if (!openSet.contains(neighbor)) {
                    openSet.add(neighbor);
                }
            }
        }
    }
    return null;
}

List<Node> reconstructPath(Map<Node, Node> cameFrom, Node current) {
    List<Node> path = new ArrayList<>();
    path.add(current);
    while (cameFrom.containsKey(current)) {
        current = cameFrom.get(current);
        path.add(0, current);
    }
    return path;
}

double heuristic(Node node1, Node node2) {
    return Math.sqrt(Math.pow(node1.x - node2.x, 2) + Math.pow(node1.y - node2.y, 2));
}

double distance(Node node1, Node node2) {
    return heuristic(node1, node2);
}