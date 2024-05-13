## C# Database Access Class with CRUD Operations and DataGridView Integration

**Description:**

This C# class, VeriTabaniH offers functionalities for interacting with Microsoft Access databases (.accdb files) using ADO.NET. It provides methods for:

* Establishing connections to Access databases
* Filling a DataGridView control with data from a specific table
* Performing CRUD (Create, Read, Update, Delete) operations on tables
* Retrieving selected values from a DataGridView (ID or column header)
* Handling connections with a Hashtable for potential use with ComboBoxes (commented out)

**Features:**

* Leverages the OleDbConnection, OleDbDataAdapter, OleDbCommand, and DataSet classes for database interactions.
* Employs error handling with try-catch blocks to display informative messages using MessageBox.Show.
* Includes a Calisma variable to track database operations and potentially optimize data retrieval in the DataGridView.

**Command functions**

* Doldur: Retrieves access data and assigns it to datagridview
* Ekle: Allows us to enter data
* Sil: Allows me to delete data
* Güncelle: Allows me to update data
* Secili: Selects the desired data
* Baglanti: Allows us to connect to the database
