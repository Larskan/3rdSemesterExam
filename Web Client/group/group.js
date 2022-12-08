function CreateTripTableElement(TripName, DateOfCreation, Sum )
{
    const TableBody = document.getElementById('TripTableBody');

    let row = document.createElement('tr'); //creates new table row

    //creates three new table entries with names corresponding to the arguments
    let NameCell = document.createElement('td');
    let DateCell = document.createElement('td');
    let SumCell = document.createElement('td');

    let NameHolder = document.createElement('a'); //creates an anchor type element so that we can link the name to a new page

    /*
    this is where we need to figure out a way to link it with a procedurally generated html that loads the proper group in
    */
    NameHolder.href = '#';

    NameCell.appendChild(NameHolder);

    //sets the text of these to the arguments
    NameHolder.innerHTML = TripName;
    DateCell.innerHTML = DateOfCreation;
    SumCell.innerHTML = Sum;

    row.appendChild(NameCell);
    row.appendChild(DateCell);
    row.appendChild(SumCell);



    TableBody.appendChild(row); //finally appends child to display it
}

function CreateMemberTableElement(username){

    const TableBody = document.getElementById('UserTableBody');
    let row = document.createElement('tr');
    let NameCell = document.createElement('td');
    let NameHolder = document.createElement('a');

    NameHolder.href = '#';

    NameHolder.innerHTML = username;

    NameCell.appendChild(NameHolder);
    row.appendChild(NameCell);
    TableBody.appendChild(row);
}

//replace this with actual code
function PopulateTripTable()
{
    CreateTripTableElement('Trip To Spain','16.06.2012','$30.000,00');
    CreateTripTableElement('Moms birthday party','12.02.2015','$240.000,00');
    CreateTripTableElement('Cat food','01.01.1980','$-2.147.483.647,00');
    TripSetter('tblTrips','1')

}

function PopulateMemberTable(){
    loadMember('1'); // replace with a sort of for each loop that gets all of the users inside of a group 

    for(i = 0; i<50;i++)
    {
        loadMember('1');
    }
}

/** loads a member into the name table based on id by handing it over to NameSetter with the params tblUser and fldFirstname*/
async function loadMember(id)
{
    NameSetter('tblUsers',id,'fldFirstName')    
}

//i have an issue with this, in the form that i cannot conveniently return the data from the query
//I would like to do so as it would allow for firstName+lastName display for clarity 
async function NameSetter(table, id, field)
{
    url = "https://localhost:7002/"+table+"/"+id
    await fetch(url)
    .then((response) => response.json())
    .then((data) => CreateMemberTableElement(data[field]))
}

async function TripSetter(table, id)
{
    url = "https://localhost:7002/"+table+"/"+id
    await fetch(url)
    .then((response) => response.json())
    .then((data) => CreateTripTableElement('Testing the water','no date',data['fldSum']))
}
