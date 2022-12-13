var buffer;

function runCode()
{
    CreateObject()
    SetObjectValue()
    DisplayObjectValue()
    SaveObjectAsCookie()
    ShowCookie()
}

function CreateObject()
{
    buffer = new Object()
}

function SetObjectValue()
{
    buffer.Value = 'random Value'
}

function DisplayObjectValue()
{
    root = document.getElementById('CookieDisplay')
    let node = document.createElement('p');
    node.innerHTML = 'The buffered value: '+buffer.Value;
    root.appendChild(node)

}

function SaveObjectAsCookie()
{
    string = JSON.stringify(buffer)
    document.cookie =  string+"; expires=Sun, 1 January 2030 12:00:00 UTC; path=/";
}

function ShowCookie()
{
    console.log(document.cookie)
    root = document.getElementById('CookieDisplay')
    let node = document.createElement('p');
    node.innerHTML = 'Cookie: '+document.cookie;
    root.appendChild(node)
}



