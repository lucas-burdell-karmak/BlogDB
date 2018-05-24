namespace The_Intern_MVC.Models
{
    // BlogOperation enum - defines blog program operations available to the user
    public enum BlogOperation
    {
        invalid = 0, // invalid operation
        viewSingle = '1', // for viewing a single Post object
        viewAll = '2',    // for viewing all Post objects
        viewAllAuthors = '3', // for viewing all unique Author values
        add = '4',        // for adding a Post object
        edit = '5',      // for editing a Post object
        delete = '6',     // for deleting a Post object
        exit = 'x',        // for exiting the program
        
    }

}
