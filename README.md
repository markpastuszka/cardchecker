# cardchecker

Made in VS2019, SQL Server 2017, .NET Core 2.2, Node, Angular

Areas I'm not happy with:
- I got most of the way there with the DB solution suggested but hit two snags which would require bodges/breaking away from the given brief/potentially risky code to fix, hence why I have stuck with the one DB but left the code in to demo where I was going:
  - To get it to return specifically the requested product as opposed to all eligible products
    - I could have just returned the first product that matched using approvedProducts.First()
    - Considered returning the product with the lowest APR rate
    - I could have returned the entire list of products and maybe had custom logic to deal with which one to display
  - Pulling the criteria from the DB
    - I ran out of time to be able to turn the strings from the DB into actual statements for validation
    - My understanding is that this could be done either through something like CSharpScript or could even be done through the DB itself, but was conscious of potential security risks such as injection.
- I don't like the use of foreaches in the DBAccessService. I'd much rather do some LINQ magic but unfortunately the conversion from DataTable to Enumerable is only being added to .NET Core as part of version 3.0. (it's existed in .NET itself for a while!)
- Angular Form validation is very basic (just checks for all fields being populated)
  - Needs some custom date verification - I've made sure in the backend that it will return ineligible if you are in the future/before 1900, but before 1753 breaks the DB
  - Angular forms has the ability for custom validators.
  - Other pickers such as those available with Bootstrap and MatDatePicker I believe have a bit more flexibility for this sort of stuff, plus look prettier.
- Forgot until I'd uploaded this that I'd hardcoded the SQL connection string! Change it at the top of the DBAccessService.
Here's the SQL you'll need to run to get what's there working:
 
CREATE TABLE APPLICATION_HISTORY (
    id int IDENTITY(1,1) PRIMARY KEY,
    forename varchar(100) NOT NULL, 
    surname varchar(100) NOT NULL,
    dob datetime NOT NULL,
    annual_income money NOT NULL,
    product_id varchar(100) NOT NULL
)

Here's my attempt at the DB structure I'd LIKE to have implemented, as discussed - This seems to work okay:

CREATE TABLE CRITERIA (
    id INT IDENTITY(1,1) PRIMARY KEY,
    criteria varchar(100) NOT NULL
)

CREATE TABLE PRODUCTS (
    id int IDENTITY(1,1) PRIMARY KEY,
    product_name varchar(100) NOT NULL,
    promotional_msg text NOT NULL,
    apr_rate float NOT NULL
)

CREATE TABLE APPLICATION_HISTORY (
    id int IDENTITY(1,1) PRIMARY KEY,
    forename varchar(100) NOT NULL, 
    surname varchar(100) NOT NULL,
    dob datetime NOT NULL,
    annual_income money NOT NULL,
    product_id INT FOREIGN KEY REFERENCES PRODUCTS(id)
)

CREATE TABLE PRODUCT_CRITERIA_MAP (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT FOREIGN KEY REFERENCES PRODUCTS(id),
    criteria_id INT FOREIGN KEY REFERENCES CRITERIA(id)
)

insert into CRITERIA (criteria) values ('age >= 18');
insert into CRITERIA (criteria) values ('AnnualIncome >= 30000');

insert into PRODUCTS (product_name, promotional_msg, apr_rate)
VALUES ('Barclaycard', 'You buy one, you get one free!', 5.0)

insert into PRODUCTS (product_name, promotional_msg, apr_rate)
VALUES ('Vanquis', 'Free pizza!', 50.0)

insert into PRODUCT_CRITERIA_MAP(product_id, criteria_id)
values (1, 1)

insert into PRODUCT_CRITERIA_MAP(product_id, criteria_id)
values (1, 2)

insert into PRODUCT_CRITERIA_MAP(product_id, criteria_id)
values (2, 1)
