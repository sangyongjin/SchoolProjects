#include <iostream>
using namespace std;

struct NodeType {
	
   int info;
   NodeType * nextPtr;
};
NodeType *AddAtPosition(NodeType *head, int data, int position)
{
	if (head == NULL)
		return head = new NodeType({ data, NULL });
	if (position == 0)
		return head = new NodeType({ data, head->nextPtr });

	// strict checking as my habit
	if (position < 0)
		return NULL;

	NodeType *root = head;
	while (root->nextPtr && --position)
		root = root->nextPtr;
	root->nextPtr = new NodeType({ data, root->nextPtr });
	return head;
}
void insert(NodeType *&head, int value)
{
    NodeType *currentPtr;
    currentPtr = new NodeType;
    currentPtr->info = value;
    currentPtr->nextPtr = head;
    head = currentPtr;
}
void insertAtEnd(NodeType *&headPtr, int value)
{   if (headPtr == NULL)
    {
        headPtr = new NodeType;
		headPtr ->info = value;
}
    else
    {
        insertAtEnd(headPtr->nextPtr, value);
    }
}

int DeleteListRecursion(NodeType*& list){
    if(list == NULL)
        return 0;
    else
     DeleteListRecursion(list->nextPtr);
     delete(list);
     list = NULL;
}
bool isEmpty(NodeType *head){

return head == NULL;
}
int sum(NodeType *head) 
{
	if(head == NULL)
		return 0;
	else
		return head->info + sum(head->nextPtr);
}

void printList(NodeType *head)
{
	
	NodeType *currentPtr = head;
	if (isEmpty(head))
	{
		cout << "The list is empty!" << endl;
	}
	else
	{
		
	while (currentPtr!=NULL)
	{
		cout << currentPtr->info <<"   " << endl;
		currentPtr = currentPtr->nextPtr;
	}
	}
	
}
NodeType* copyList(NodeType*& head, NodeType*& copy) {
	
	if (head == NULL) return copy =NULL;
	else{
	copy = new NodeType;
	copy->info = head->info;
	copy->nextPtr = head->nextPtr;
	return copy;}
}
void count(NodeType *&head)
{
	NodeType *temp;
	int length = 0;
	temp = head;
	while (temp != NULL)
	{
		length++;
		temp = temp->nextPtr;
	}
	cout << length << endl;
}
int main()
{
    NodeType *head= NULL;
	NodeType *copy= NULL;
	
    int choice;
    int value;
	int value1;
	int value2;
	int pos;

    while (true) {
        cout << "1. Add" << endl;
        cout << "2. Print" << endl;
        cout << "3. Quit" << endl;
		cout << "4. delete list" << endl;
		cout << "5. Copy List " << endl;
		cout << "6. print copied List " << endl;
		cout << "7. count node " << endl;
		cout << "8. insert at the end " << endl;
		cout << "9. Sum all " << endl;
		cout << "10. insert at a position " << endl;
        cout << "Choose: ";
        cin >> choice;
        switch (choice) {
            case 1:
                cout << "Enter the value: ";
                cin >> value;
                insert(head, value);
                break;
            case 2:
                printList(head);
                break;
            case 3:
                return 0;
			case 4:
				DeleteListRecursion(head);
				break;
			case 5:
				copyList(head, copy);
				break;
			case 6:
				printList(copy);
				break;
			case 7:
			count(head);
			break;
			case 8:
				cout << "Enter a value : ";
				cin >> value1;
				insertAtEnd(head, value1);
				break;
			case 10:
				cout << "Enter a value : ";
				cin >> value2;
				cout << "Enter a position: ";
				cin >> pos;
				AddAtPosition(head,value2, pos);
				break;
			case 9:
				cout << "The sum is " << sum(head) << endl;;
				break;
        }

        cout << endl;
    }
}


