var deckHTML;
var cardHTML; //each hyperlink html to each card all concatenated into one location.
var counter = 0;
var counter2 = 0;
var urlList;
var cardList;
var cardURLS = new Array();

var mainPath = "http://codexcarddb.com";

const proxyurl = "https://cors-anywhere.herokuapp.com/";
var corsProxy = "https://cors.now.sh/";

$(document).ready(function () {

    deckHTML = document.createElement('html');
    cardHTML = document.createElement('html');

    //Populate with a list of all the card colors
	urlList = [
        "http://codexcarddb.com/color/green",
        "http://codexcarddb.com/color/blue",
        "http://codexcarddb.com/color/red",
        "http://codexcarddb.com/color/white",
        "http://codexcarddb.com/color/black",
        "http://codexcarddb.com/color/purple",
        "http://codexcarddb.com/color/neutral"
    ];
        
	urlList.forEach(url => 
		fetch(proxyurl + url)
			.then(response => response.text())
            .then(contents => fillHTML(contents))
    );
});

//Take the HTML we got using above function and 'pastes' it into our current HTML document for us to use
function fillHTML(contents) {
    counter++;
    deckHTML.innerHTML += contents;

    if (counter === urlList.length) {
        PopulateDropDown();
    }
}

//Gets all cards and populates their names into a dropdown
function PopulateDropDown() {
    cardList = deckHTML.getElementsByClassName("css-8uhtka");
    var sel = document.getElementById('cardSelect');

    for (var i = 0; i < cardList.length; i++) {
        var opt = document.createElement('option');
        opt.id = i;
        opt.value = cardList[i].childNodes[0].text;
        opt.innerHTML = cardList[i].childNodes[0].text;
        sel.appendChild(opt);
    }
    PopulateCardURLs();
}

//Fill list of all hyperlinks to all cards
function PopulateCardURLs() {

    for (var i = 0; i < cardList.length; i++) {
        var cardPath = mainPath + cardList[i].childNodes[0].getAttribute('href');
        var cardPlusProxyPath = proxyurl + cardPath;
        cardURLS[i] = cardPlusProxyPath;
    }

    GetCardHTMLS();
}

function GetCardHTMLS() {
    cardURLS.forEach(cardURL =>
        fetch(cardURL)
            .then(response => response.text())
            .then(contents => fillCardHTML(contents))
    );
}

//Take the HTML we got using above function and 'pastes' it into new element cardHTML
function fillCardHTML(contents) {
    counter2++;
    progressBarUpdate(counter2);
    cardHTML.innerHTML += contents;

    if (counter2 === cardURLS.length) {
        PopulateTable();
    }
}

function PopulateTable() {
    cardCount = cardHTML.getElementsByClassName("css-low8xa");
    //alert(cardCount.length);
    var tab = document.getElementById('cardsTable');

    for (var i = 0; i < 330; i++) { //rows
        var row = document.createElement('tr');
        tab.appendChild(row);

        var col = new Array();
        for (var j = 0; j < 12; j++) {
            col[j] = document.createElement('td');
            var cardName = cardHTML.getElementsByClassName("css-ovwgyn")[i].innerText;
            var cardInfo = cardHTML.getElementsByClassName("css-1wlrlh5")[i]; 
            var str = cardInfo.childNodes[0].innerText.split("\u2022"); //splits by bullets

            var links = cardInfo.getElementsByTagName("a");
            var bquotes = cardInfo.getElementsByTagName("blockquote");

            switch (j) {
                case 0: //Card Name
                    col[j].innerHTML = '"' + cardName + '"';
                    break;
                case 1: //Color
                    col[j].innerHTML = links[0].innerText;
                    break;
                case 2: //Spec
                    var spec = links[0].parentNode.innerText.split("\u2022")[1];
                    if (spec === undefined)
                        col[j].innerHTML = links[0].innerText;
                    else
                        col[j].innerHTML = spec.split(" ")[1];
                    break;
                case 3: //Tech
                    var tech = links[0].parentNode.innerText.split("\u2022")[1];
                    if (tech === undefined)
                        col[j].innerText = "Tech 0";
                    else if (tech.split(" ")[3] !== undefined)
                        col[j].innerText = tech.split(" ")[2] + " " + tech.split(" ")[3];
                    else
                        col[j].innerText = tech.split(" ")[2];
                    break;
                case 4: //Type
                    var type = str[0].split("\u2014")[0];
                    col[j].innerHTML = type;
                    break;
                case 5: //Sub-Type
                    var subType = str[0].split("\u2014")[1];
                    if (subType !== undefined)
                        col[j].innerHTML = subType;
                    else
                        col[j].innerHTML = "NA";
                    break;
                case 6: //Cost
                    var cost = str[1].split("Cost: ")[1];
                    col[j].innerHTML = cost;
                    break;
                case 7: //Attack
                    if (str[2] !== undefined && str[2].includes("ATK:"))
                        col[j].innerHTML = str[2].split("ATK: ")[1];
                    else
                        col[j].innerHTML = "NA";
                    break;
                case 8: //HP
                    if (str[2] !== undefined && str[2].includes("HP:"))
                        col[j].innerHTML = str[2].split("HP: ")[1];
                    else if (str[3] !== undefined && str[3].includes("HP:"))
                        col[j].innerHTML = str[3].split("HP: ")[1];
                    else
                        col[j].innerHTML = "NA";
                    break;
                case 9: //Keywords
                    col[j].innerText = "NA";
                    break;
                case 10: //Rules Text
                    if (bquotes[0] !== undefined) {
                        var str1 = bquotes[0].innerText;
                        var str2 = str1.replace(/"/g, "'");
                        var str3 = str2.replace(/◎/g, "[target]");
                        var str4 = str3.replace(/⤵/g, "[tap]");
                        var str5 = str4.replace(/\u2460/g, "[1G]");
                        var str6 = str5.replace(/②/g, "[2G]");
                        var str7 = str6.replace(/④/g, "[4G]");
                        var str8 = str7.replace(/⑧/g, "[8G]");
                        var str9 = str8.replace(/⓪/g, "[0G]");
                        var str10 = str9.replace(/③/g, "[3G]");
                        var str11 = str10.replace(/⑤/g, "[5G]");
                        var str12 = str11.replace(/→/g, "[then]");
                        col[j].innerText = '"' + str12 + '"';
                    }                        
                    else
                        col[j].innerText = "NA";
                    break;
                case 11: //Flavor Text
                    col[j].innerText = "NA";
                    break;
                default:
                    col[j].innerHTML = "NA";
            }
            row.appendChild(col[j]);
        }
    }
}
function download_csv(csv, filename) {
    var csvFile;
    var downloadLink;

    // CSV FILE
    csvFile = new Blob([csv], { type: "text/csv" });

    // Download link
    downloadLink = document.createElement("a");

    // File name
    downloadLink.download = filename;

    // We have to create a link to the file
    downloadLink.href = window.URL.createObjectURL(csvFile);

    // Make sure that the link is not displayed
    downloadLink.style.display = "none";

    // Add the link to your DOM
    document.body.appendChild(downloadLink);

    // Lanzamos
    downloadLink.click();
}

function export_table_to_csv(filename) {
    var csv = [];
    var rows = document.querySelectorAll("table tr");

    for (var i = 0; i < rows.length; i++) {
        var row = [], cols = rows[i].querySelectorAll("td, th");

        for (var j = 0; j < cols.length; j++)
            row.push(cols[j].innerText);

        csv.push(row.join(","));
    }

    // Download CSV
    download_csv(csv.join("\n"), filename);
}

function progressBarUpdate(count) {
    var elem = document.getElementById("myBar");
    var width = count;
    elem.style.width = width * .34 + '%';
    elem.innerHTML = " " + width + '/330 Cards Loaded';
}