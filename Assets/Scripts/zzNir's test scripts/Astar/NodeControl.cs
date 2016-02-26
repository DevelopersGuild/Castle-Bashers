using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NodeControl : MonoBehaviour {
	
	public string nodeTag;
    public LayerMask collisionMask;

    class Point
	{
		Vector3 m_pos;
		char m_state = 'u';
		float m_score = 0;
		Point m_prevPoint;
		
		List<Point> m_connectedPoints = new List<Point>();
		List<Point> m_potentialPrevPoints = new List<Point>();
		
		public Point(Vector3 pos, char state = 'u')
		{
			m_pos = pos;
			m_state = state;
		}
		
		public char GetState()
		{
			return m_state;
		}

        
		public Vector3 GetPos()
		{
			return m_pos;
		}
		
		public List<Point> GetConnectedPoints()
		{
			return m_connectedPoints;
		}
		
		public Point GetPrevPoint()
		{
			return m_prevPoint;
		}
		
		public float GetScore()
		{
			return m_score;
		}
		
		public List<Point> GetPotentialPrevPoints()
		{
			return m_potentialPrevPoints;
		}
		
		public void AddConnectedPoint(Point point)
		{
			m_connectedPoints.Add(point);
		}
		
		public void AddPotentialPrevPoint(Point point)
		{
			m_potentialPrevPoints.Add(point);
		}
		
		public void SetPrevPoint(Point point)
		{
			m_prevPoint = point;
		}
		
		public void SetState(char newState)
		{
			m_state = newState;
		}
		
		public void SetScore(float newScore)
		{
			m_score = newScore;
		}

        public void reset()
        {
            m_connectedPoints.Clear();
            m_potentialPrevPoints.Clear();
            m_prevPoint = null;
        }
    }

   

    public List<Vector3> Path(Vector3 startPos, Vector3 targetPos, float zDiff = -1)
	{
		//Can I see the exit
		float exitDistance = Vector3.Distance(startPos, targetPos);
        //RaycastHit hitInfo;
        Physics.Raycast(startPos, targetPos, exitDistance, collisionMask);
        if (!Physics.Raycast(startPos, targetPos - startPos, exitDistance))
		{
			List<Vector3> path = new List<Vector3>();
			path.Add(startPos);
            if (zDiff == -1 || targetPos.z - startPos.z <= zDiff)
                path.Add(targetPos);
            else
                path.Add(new Vector3(startPos.x, startPos.y, targetPos.z));

			return path;
		}
		
		GameObject[] nodes = GameObject.FindGameObjectsWithTag(nodeTag);
		List<Point> points = new List<Point>();
		foreach (GameObject node in nodes)
		{
			Point currNode = new Point(node.transform.position);
			points.Add(currNode);
		}
		
		Point endPoint = new Point(targetPos, 'e');
		
		/***Connect them together***/
		foreach(Point point in points) //Could be optimized to not go through each connection twice
		{
			foreach (Point point2 in points)
			{
				float distance = Vector3.Distance(point.GetPos(), point2.GetPos());
				if (!Physics.Raycast(point.GetPos(), point2.GetPos() - point.GetPos(), distance))
				{
					//Debug.DrawRay(point.GetPos(), point2.GetPos() - point.GetPos(), Color.white, 1);
					point.AddConnectedPoint(point2);
				}
			}
			float distance2 = Vector3.Distance(targetPos, point.GetPos());
			if (!Physics.Raycast(targetPos, point.GetPos() - targetPos, distance2))
			{
				//Debug.DrawRay(targetPos, point.GetPos() - targetPos, Color.white, 1);
				point.AddConnectedPoint(endPoint);
			}
		}
		
		//points startPos can see
		foreach (Point point in points)
		{
			float distance = Vector3.Distance(startPos, point.GetPos());
			if (!Physics.Raycast(startPos, point.GetPos() - startPos, distance))
			{
				//Debug.DrawRay(startPos, point.GetPos() - startPos, Color.white, 1);
				Point startPoint = new Point(startPos, 's');
				point.SetPrevPoint(startPoint);
				point.SetState('o');
				point.SetScore(distance + Vector3.Distance(targetPos, point.GetPos()));
			}
		}
		
		//Go through until we find the exit or run out of connections
		bool searchedAll = false;
		bool foundEnd = false;
		
		while(!searchedAll)
		{
			searchedAll = true;
			List<Point> foundConnections = new List<Point>();
			foreach (Point point in points)
			{
				if (point.GetState() == 'o')
				{
					searchedAll = false;
					List<Point> potentials = point.GetConnectedPoints();
					
					foreach (Point potentialPoint in potentials)
					{
						if (potentialPoint.GetState() == 'u')
						{
							potentialPoint.AddPotentialPrevPoint(point);
							foundConnections.Add(potentialPoint);
							potentialPoint.SetScore(Vector3.Distance(startPos, potentialPoint.GetPos()) + Vector3.Distance(targetPos, potentialPoint.GetPos()));
						} else if (potentialPoint.GetState() == 'e')
						{
							//Found the exit
							foundEnd = true;
							endPoint.AddConnectedPoint(point);
						}
					}
					point.SetState('c');
				}
			}
			foreach (Point connection in foundConnections)
			{
				connection.SetState('o');
				//Find lowest scoring prev point
				float lowestScore = 0;
				Point bestPrevPoint = null;
				bool first = true;
				foreach (Point prevPoints in connection.GetPotentialPrevPoints())
				{
					if (first)
					{
						lowestScore = prevPoints.GetScore();
						bestPrevPoint = prevPoints;
						first = false;
					} else
					{
						if (lowestScore > prevPoints.GetScore())
						{
							lowestScore = prevPoints.GetScore();
							bestPrevPoint = prevPoints;
						}
					}
				}
				connection.SetPrevPoint(bestPrevPoint);
			}
		}
		
		if (foundEnd)
		{
			//trace back finding shortest route (lowest score)
			List<Point> shortestRoute = null;
			float lowestScore = 0;
			bool firstRoute = true;
			
			foreach (Point point in endPoint.GetConnectedPoints())
			{
				float score = 0;
				bool tracing = true;
				Point currPoint = point;
				List<Point> route = new List<Point>();
				route.Add(endPoint);
				while(tracing)
				{
					route.Add(currPoint);
					if (currPoint.GetState() == 's')
					{
						if (firstRoute)
						{
							shortestRoute = route;
							lowestScore = score;
							firstRoute = false;
						} else
						{
							if (lowestScore > score)
							{
								shortestRoute = route;
								lowestScore = score;
							}
						}
						tracing = false;
						break;
					}
					score += currPoint.GetScore();
					currPoint = currPoint.GetPrevPoint();
				}
			}
			
			shortestRoute.Reverse();
			List<Vector3> path = new List<Vector3>();
			foreach (Point point in shortestRoute)
			{
				path.Add(point.GetPos());
			}
			return path;
		} else
		{
			return null;
		}
	}
}