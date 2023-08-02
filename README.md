# TODO List Sample Application

Upon loading, the app will present a simple, two-page interface powered by SQLite. The first page will contain Todo Lists. Each Todo List can contain Todo Items.

- Select a Todo List to view its Todo Items
- Todo Items can be completed via the RadioButtons.
- Todo Items and Todo Lists can be edited and deleted using Swipe-left and selecting the appropriate option.

Things I probably would have done with more time:

Manage strings for localization using a Resx file
not reload lists when a list item has changed
Paginate or otherwise progressively load long lists
animations esp when a todo item is completed
The two Repository classes are really more like Service layers; and the Sqlite extensions / TodoDatabase object is more like the repository layer. 
logger isn't logging to debug window as I expected it to
debug why the emptyview doesn't always show up in todo item view (suspect its a MAUI bug)