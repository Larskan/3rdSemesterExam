var URLRoot = 'https://localhost:7002/';
var UserID = '1'


async function setup()
{  
  NameSetter(UserID)
  LoadGraphs()
  GetExpenses()
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
        text: "World Wide Wine Production 2018"
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
        text: "World Wide Wine Production 2018"
      }
    }
  });
  
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
  let counter = 0;
  url = URLRoot+'tblUserExpenses/'
  //loop to get expenses from link
  for(i = 0; i<ExpenseArray.length;i++)
  {
    await fetch(url+ExpenseArray[i].fldExpenseId)
    .then((response)=>response.json())
    .then((data)=>
    { 
    counter+=data['fldExpense'];
    CreateExpenseElement(data['fldExpense'])
    })
  }

  SetSum(counter)

}

async function SetSum(sum)
{
  document.getElementById('SumHeader').innerHTML = sum + ' Currency'
}


async function CreateExpenseElement(number)
{
  let node = document.getElementById('expenses')
  let expense = document.createElement('p')
  expense.innerHTML = number + ' Currency; Date; User;'
  node.appendChild(expense)
}



async function NodeHandler(NodeName,TargetText)
{
  document.getElementById(NodeName).innerHTML = TargetText
}

async function MakeGraph()
{
  //this is where it would await a fetch for data


}

