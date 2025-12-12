**Municipal Services Application Specifications**
=================================================
- Project type: C# Windows Forms (.NET Framework)
- Target Framework: .NET Framework 4.8.0 (or 4.7+)

**C# Files Included in Visual Studio Project**
===============================================
- AVLNode.cs
- AVLTree.cs
- BST.cs
- CheckStatus.cs
- Event.cs
- Graph.cs
- IssueNode.cs
- LocalEvents.cs
- MenuScreen.cs
- MinHeap.cs
- Program.cs
- ReportIssues.cs
- ServiceIssue.cs
- SharedDataModel.cs

**How to Compile and Run**
==========================
1.	Open Visual Studio 2022.
2.	Open the project named " MunicipalServices"
3.	Ensure the Program.cs is the default start-up file and is present in the project.
4.	Click Build and select Clean Solution
5.	Click Build Solution and run the .exe

**Key Features of Application and How to Use Features**
=======================================================

Report Issue
------------

Usage:
The application encapsulates an interface for users to handle issues and service requests of a local municipality. Thus a feature was created to 
allow users to report local municipal issues, thus requesting service to solve the issue.

How to Use
The “reporting” of an issue is presented by the completion of a form, which collects all necessary information for the Municipal Workers to 
investigate and resolve the reported issue. The form is broken down into 4 main sections, namely the Location, Category, Description and 
Evidence Attachment.

Form Sections:
- Location: The user is required to provide their location and it is imperative for a user to specify their respective ward so issues can be 
directed to the appropriate ward councillor. Thus the location is captured using a TextBox and Error Handling is implemented to ensure the 
user specified their ward in the entry. The form will not submit unless the user has specified a ward. 

- Category: By providing a category for the issue, the municipal heads would be able to delegate the service to the appropriate departments 
for resolution of the issue. Due to there being a limited number of categories, the category is captured using a ComboBox, however there is 
implementation for the form to capture the possibility of the user needing assistance with a different category. If the user selects the “Other” 
option, an input dialog box will capture the category the user needs assistance with and displays it back to the user.

- Description: The user is required to provide a description of the issue they are experiencing so municipal workers know exactly what to work 
on. Thus the description is captured using a RichTextBox.

- Evidence Attachment: It’s not necessarily compulsory for a user to provide evidence” of an issue reported. However, it will greatly benefit 
the municipal workers and it might aid users that might struggle in verbally describing the issue. Thus the evidence is captured using a 
OpenFileDialog which will allow the user to attach any images they have; the OpenFileDialog is set to only accept valid images such as jpeg and png files.

Local Events Management
-----------------------

Usage:
Another feature on this application is the maintenance and viewing of local events in the community. This interface allows users to track local 
events currently happening. The feature displays all events in chronological order making it easy to view and understand, additionally there 
are several search and filter options to give the user the ability to search for specific information instead of reading through endless entries.

How to Use:
The local events are displayed to the user in a DataGridView and the user is given several options to filter the events, namely by Keyword, Category and Date.

Filter Options:
- Filter by Keyword: In many cases, a user might not know the exact title of an event, but could remember keywords or important phrases. 
Thus the form provides the user with an option to select a keyword which the application then searches all Event Titles for matches to this 
keyword and displays all appropriate matches. This keyword is captured using a TextBox.

- Filter by Category: There are a fixed amount of categories for events thus the category filter is captured by a ComboBox. The category is 
checked against a list of acceptable categories and events are displayed only if they contain the selected category.

- Filter by Date: A DateTime picker is provided to the user to select a date and all events taking place on the given date will be displayed. 
There is no specific restriction to picking dates for filtering.

- Upcoming events: The user also has an option to only view upcoming events, which is pre-set to all events within a week of the current date. 
All events within a week (7 days) of the current date is  filtered out and displayed to the user.

- Recently Viewed Events: Each time a user searches or filters an event, the event is internally recorded/ Many users might want to view popular 
or recently searched events, these events internally stored is then displayed  to the user.

The user also has an option to combine the usage of filters, but this provides a more specific result, rather than a general result. For example, 
the user can search a keyword within a specific category or on a specific date. There is also an option to clear all filters and revert to 
displaying all events.

Service Status Update
---------------------

Usage
An application that allows you to report an issue but gives no feedback or tracking ability is ultimately useless, thus a feature to view the 
status of issues is offered on this application. The requests are stored by their RequestID, making it easy to traverse and search through for 
users to check updates on a specific request. Each issue reported in the first form (Report Issues) is added to this list of requests displayed 
on this form.

How to Use
Similar to the Local Events, the requests are displayed to the user in a DataGridView and the user is given several options to search for their 
request, namely by RequestID and by BFS.

Search Options:
- The requests are stored generically in a Dictionary for records purposes but they are also stored in a Binary Search Tree and AVL Tree (both 
structures were used for comparison purposes). A user provides the Request ID of the issue they had reported and the application makes use of AVL 
and BST searched to find the specific request. This request is then displayed back to the user. Error handling is put in place to indicate whether 
a request cannot be found.

- All request/reports will have dependencies on another due to the order in which they need to be resolved. Thus a graph structure was created to 
instantiate dependencies between request. Should the user wish to view the order in which requests are being processed, the user can click the 
“Display Dependencies” button.

*Note: There are at least 3 requests pre-loaded (hard coded) for demo purposes but current issues are also added as submitted.*

**Improvements based on Lecturer Feedback**
===========================================

My lecturer had mentioned 3 main areas of concern in my project, namely, poor GUI design, usage of AI and lack of advanced data structure. To overcome 
these issues, a running theme was added in colour scheme and municipality logo to give the impression of a “whole” application theme and a more user-friendly 
GUI. Additionally, AI was not used for the main development of the supplementary submission, instead only consulted for algorithm and theory explanations. 
Lastly, advanced data structures, such as, BST, AVLTree, Dictionary, Queues, Stacks, etc. were implemented throughout the POE.

**YouTube Link**
================

https://youtu.be/rxvkx_5fZQE 
