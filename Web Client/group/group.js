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

}

function PopulateMemberTable(){
    CreateMemberTableElement('John');
    CreateMemberTableElement('Mary');
    CreateMemberTableElement('July');

}