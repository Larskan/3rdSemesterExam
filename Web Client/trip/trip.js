var URLRoot = 'https://localhost:7002/';
var UserID = '1'
var ids = [];

async function setup()
{  
  NameSetter(UserID)
  LoadGraphs()
  GetExpenses()
}

async function NameSetter(id)
{
  let url = URLRoot+'tblTrips/'+id
  await fetch(url)
  .then((response)=>response.json())
  .then((data)=>NodeHandler('NameHeader',data['fldTripName']))
}

async function GetExpenses()
{
  let obj;

  let url = URLRoot+'tblTripToUserExpenses'
  
  //fetch
  await fetch(url)
  .then((response)=>response.json())
  .then((data) => obj=data)
  
  //loop to get expenses and trip links
  let target = 4; // change with the target ID from Cookie

  let i = 0; //counters
  let j = 0;

  var result = [];

  do{
    if(obj[i].fldTripId==target)
    {
      result[j]= obj[i]
      j++
    }
    i++
  } while(obj[i]!=null)

  HandleExpenses(result)
  
  
}

async function HandleExpenses(ExpenseArray)
{
  let counter = 0; //counter for sum of expenses

  url = URLRoot+'tblUserExpenses/'

  for (i = 0; i < ExpenseArray.length; i++)
  {
    await fetch(url+ExpenseArray[i].fldExpenseId)
    .then((response)=>response.json())
    .then((data)=>
    { 
    counter+=data['fldExpense']; //adds to counter to sum the expenses
    CreateExpenseTableElement(data['fldExpense'], data['fldDate'], data['fldNote'], data['fldUserId']) //creates an element with the given data elements
    ids[i] = data['fldUserId'] 

  })
    
  }
  SetSum(counter) //sets sum of expenses
  SetNames();

}

function LoadGraphs() 
{
  
  var xValues = ["Italy", "France", "Spain", "USA"];
  var yValues = [55, 49, 44, 24];
  var barColors = [
    "#b91d47",
    "#00aba9",
    "#2b5797",
    "#e8c3b9",
  ];

  new Chart("BarChart", {
    type: "bar",
    data: {
      labels: xValues,
      datasets: [{
        backgroundColor: barColors,
        data: yValues
      }]
    },
    options: {
      title: {
        display: true,
        text: "Title of BarChart"
      }
    }
  });
  
  new Chart("PieChart", {
    type: "pie",
    data: {
      labels: xValues,
      datasets: [{
        backgroundColor: barColors,
        data: yValues
      }]
    },
    options: {
      title: {
        display: true,
        text: "Title of PieChart"
      }
    }
  });
}

async function SetSum(sum)
{
  document.getElementById('SumHeader').innerHTML = sum
}

async function SetNames()
{
  ids.forEach(async element => {
    url = URLRoot+'tblUsers/'+element
    let innerHTML;

  await fetch(url)
  .then((response) => response.json())
  .then((data) => innerHTML = data['fldFirstName']+' '+data['fldLastName'])

  let targetNode = 'UserID='+element
  await NodeHandler(targetNode,innerHTML)
    
  });
  
}

async function CreateExpenseTableElement(Amount, Date, Note, UserID )
{
    const TableBody = document.getElementById('ExpenseTableBody');

    let row = document.createElement('tr'); //creates new table row

    //creates three new table entries with names corresponding to the arguments
    let AmountCell = document.createElement('td');
    let UserCell = document.createElement('td');
    let DateCell = document.createElement('td');
    let NoteCell = document.createElement('td');

    //sets the text of these to the arguments
    AmountCell.innerHTML = Amount;
    UserCell.innerHTML = UserID;
    DateCell.innerHTML = Date;
    NoteCell.innerHTML = Note;

    UserCell.id = 'UserID='+UserID;

    row.appendChild(AmountCell);
    row.appendChild(UserCell);
    row.appendChild(DateCell);
    row.appendChild(NoteCell);

    TableBody.appendChild(row); //finally appends child to display it
}

async function NodeHandler(NodeName,TargetText)
{
  document.getElementById(NodeName).innerHTML = TargetText
}