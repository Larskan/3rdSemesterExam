var URLRoot = 'https://localhost:7002/';
var UserID = '1'


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
  //i feel like this one could be put outside of this function 
  
  
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
      CreateExpenseElement(data['fldExpense'], data['fldUserID'], data['fldDate'], data['fldNote']) //creates an element with the given data elements
      //TODO; edit CreateExpenseElement to make it create fields instead of plaintext, load the name instead of the id
    })
  }

  SetSum(counter) //sets sum of expenses

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
  document.getElementById('SumHeader').innerHTML = sum + ' Currency'
}

//TODO; edit CreateExpenseElement to make it create fields instead of plaintext, load the name instead of the id
async function CreateExpenseElement(number,UserID,Date,Note)
{
  let node = document.getElementById('expenses')
  let expense = document.createElement('p')
  expense.innerHTML = number + ' Currency;' + UserID+' '+Date+' '+Note
  node.appendChild(expense)
}



async function NodeHandler(NodeName,TargetText)
{
  document.getElementById(NodeName).innerHTML = TargetText
}