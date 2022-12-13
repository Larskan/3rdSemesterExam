var URLRoot = 'https://localhost:7002/';
var UserID = '1'


async function setup()
{  
  LoadSum()
  LoadName()
  LoadGraphs()
}

async function LoadSum()
{
  SumSetter(UserID)
}

async function LoadName()
{
  NameSetter(UserID)
}

async function LoadGraphs(){}

async function SumSetter(id)
{
  let url = URLRoot+'tblTrips/'+id
  await fetch(url)
  .then((response)=>response.json)
  .then((data) => NodeHandler('SumHeader',data['fldSum']))
}

async function NameSetter(id)
{
  let url = URLRoot+'tblTrips/'+id
  await fetch(url)
  .then((response)=>response.json)
  .then((data)=>console.log(data))
  //.then((data)=>NodeHandler('NameHeader',data['fldTripName']))
}


/*
have ID -> Find Group based on ID -> Find Trip based on ID -> Get Expenses based on ID -> use Expenses to calc sum and stuff
*/
async function GetExpenses()
{
  let url = URLRoot+'tblTrips'
}



async function NodeHandler(NodeName,TargetText)
{
  document.getElementById(NodeName).innerHTML = TargetText
}

