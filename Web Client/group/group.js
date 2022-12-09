var URLRoot = 'https://localhost:7002/'

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
    NameHolder.href = '../trip/trip.html';

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

    loadTrip('1');
    loadTrip('2');
    loadTrip('3');
    document.cookie = 'id: 1'
    console.log(document.cookie)
    CreateTripTableElement('Cookie Test', 'no date', '3 bucks')

}

function PopulateMemberTable(){
    loadMember('1'); // replace with a sort of for each loop that gets all of the users inside of a group 

    for(i = 0; i<9;i++)
    {
        if(i%2==1)
        {
            loadMember('1');
        } else loadMember('2');
        
    }
}

/** loads a member into the name table based on id by handing it over to NameSetter with the params tblUser, fldFirstName, fldLastName*/
async function loadMember(id)
{
    NameSetter('tblUsers',id,'fldFirstName','fldLastName')    
}

/** loads a trip into the trip table based on id by handing it over to TripSetter with the params tblTrips, id */
async function loadTrip(id)
{
    TripSetter('tblTrips',id)
}

async function NameSetter(table, id, firstname, lastname)
{
    url = URLRoot+table+"/"+id
    await fetch(url)
    .then((response) => response.json())
    .then((data) => CreateMemberTableElement(data[firstname]+' '+data[lastname]))
}

async function TripSetter(table, id)
{
    url = URLRoot+table+"/"+id
    await fetch(url)
    .then((response) => response.json())
    .then((data) => CreateTripTableElement('Testing the water','no date',data['fldSum']))
}