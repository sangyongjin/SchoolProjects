from queue import PriorityQueue
from math import sqrt
import sys


class State(object):
    def __init__(self, value, parent,
                 start = 0, goal = 0):
            self.children = []
            self.parent = parent
            self.value = value
            self.dist = 0
            twodarr = ""
        
            if parent:
#This is reconstruct the string back into a 2darray format for printing
                for i in range(len(value)):
                    twodarr += value[i]
                    if (i % 3) == 2: 
                        twodarr += "\n"
                    else:
                        twodarr += " "
                self.path = parent.path[:]
                self.path.append(twodarr)
                self.start = parent.start
                self.goal = parent.goal
            else:
                self.path = [twodarr]
                self.start = start
                self.goal = goal
            
    def out_of_place(self):
            pass
    
    def CreateChildren(self):
        	 pass

class State_Puzzle(State):
    def __init__(self, value, parent, start = 0, goal = 0):
        	super(State_Puzzle, self).__init__(value, parent, start, goal)
        	self.dist = self.out_of_place()
        
    def out_of_place(self):
        	if self.value == self.goal:
            		return 0
        	dist = 0
        	size = sqrt(len(self.goal)).real
        	for n in range (len(self.goal)):
                    if not self.value[n] == self.goal[n]:
                        dist += 1    
                    piece = self.goal[n]
                    goal_x = int(n / size)
                    goal_y = n - goal_x * size
                    value_x = int(self.value.index(piece) / size)
                    value_y = self.value.index(piece) - value_x * size
            
                    dist += abs(goal_x - value_x) + abs(goal_y - value_y)
        
        	return dist + len(self.path)
     
    def CreateChildren(self):
        size = int(sqrt(len(self.goal)).real)
        if not self.children:
            i = self.value.index('x')
            #the if statements decides the free tile that x can move to
            if not int(i % size) == size - 1:
                val = self.value
                #this code switches the "x" with the value
                #and appends the the other values to front and back
                #accordingly
                val = val[:i] + val[i+1] + val[i] + val[i+2:]
                child = State_Puzzle(val, self)
                self.children.append(child)
            
            if not int(i % size) == 0:
                val = self.value
                val = val[:i-1] + val[i] + val[i-1] + val[i+1:]
                child = State_Puzzle(val, self)
                self.children.append(child)
            
            if i < len(self.value) - size:
                val = self.value
                val = val[:i] + val[i+size] + val[i+1:i+size] + val[i] + val[i+size+1:]
                child = State_Puzzle(val, self)
                self.children.append(child)
                
            if i > size - 1:
                val = self.value
                val = val[:i-size] + val[i] + val[i-size+1:i] + val[i-size] + val[i+1:]
                child = State_Puzzle(val, self)
                self.children.append(child)
                 
class AStar_solver:
    def __init__(self, start, goal, tries):
        self.path = []
        self.visitedQueue = []
        self.priorityQueue = PriorityQueue()
        self.start = start
        self.goal = goal
        self.tries = tries
        
    def Solve(self):
        startState = State_Puzzle(self.start,
               	                   	0,
                                  	self.start,
                                  	self.goal)
        count = 0
        self.priorityQueue.put((0, count, startState))
        while(not self.path and self.priorityQueue.qsize()):
            closestChild = self.priorityQueue.get() [2]
            closestChild.CreateChildren()
            self.visitedQueue.append(closestChild.value)
            for child in closestChild.children:
                if child.value not in self.visitedQueue:
                    count += 1
                    self.tries += 1
    
                    if not child.dist:
                        self.path = child.path
                        break
                    self.priorityQueue.put((child.dist, count, child))
			
            if (self.tries/100) > 100:
                print ("Unable to find Solution")
                sys.exit(0) 
            
        if not self.path:
                print ("Goal of " + self.goal + " is not possible")
        
        return self.path

if __name__ == "__main__":
    fileStart = ""
    filestr = ""
    strstart = ""
    strgoal = ""
    tries = 0
    
    with open("puzzle.txt") as f:
            count = 0
            for line in f:
                line.strip().strip('\n').strip('0')
                if count == 3:
                        fileStart = filestr
                        filestr = ""
                else:
                        filestr += str(line)

                count += 1
                
    print ("FILE OPENED")
    print ("INITIAL")
    print (fileStart)
    print ("GOAL")
    print (filestr)
    
    print(fileStart[7])
    
    for i in range(len(fileStart)):
        if (i % 2) == 0:		
            strstart += fileStart[i]
            strgoal += filestr[i]	

                    
    print ('starting...')
    a = AStar_solver (strstart, strgoal, tries)
    a.Solve()
	
    print ("Number of steps: %i" %(len(a.path) + 1))
    
    for i in range(len(a.path)):
        print (a.path[i]) 
    
