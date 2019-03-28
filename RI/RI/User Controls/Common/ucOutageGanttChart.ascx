<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucOutageGanttChart.ascx.vb"
	Inherits="RI_User_Controls_Common_ucOutageGanttChart" %>

<script language="javascript">

function SortableTable (tableEl,maxCols) {
/**
* 
* Sortable HTML table
* http://www.webtoolkit.info/
*
**/ 
 if (tableEl==null) return false;
    this.tbody = tableEl.getElementsByTagName('tbody');
    this.thead = tableEl.getElementsByTagName('thead');
    this.originalthead = tableEl.getElementsByTagName('thead');
    //this.tfoot = tableEl.getElementsByTagName('tfoot');

    this.getInnerText = function (el) {
        if (typeof(el.textContent) != 'undefined') return el.textContent;
        if (typeof(el.innerText) != 'undefined') return el.innerText;
        if (typeof(el.innerHTML) == 'string') return el.innerHTML.replace(/<[^<>]+>/g,'');
    }

    this.getParent = function (el, pTagName) {
        if (el == null) return null;
        else if (el.nodeType == 1 && el.tagName.toLowerCase() == pTagName.toLowerCase())
            return el;
        else
            return this.getParent(el.parentNode, pTagName);
    }

    this.sort = function (cell) {

     var column = cell.cellIndex;
     if(column>maxCols) return false;
     
     var itm = this.getInnerText(this.tbody[0].rows[1].cells[column]);
        var sortfn = this.sortCaseInsensitive;
		var RegExPattern = /^(?=\d)(?:(?:(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2}))($|\ (?=\d)))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ [AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
        if (itm.match(RegExPattern)) sortfn = this.sortDate; // date format mm-dd-yyyy
        if (itm.replace(/^\s+|\s+$/g,"").match(/^[\d\.]+$/)) sortfn = this.sortNumeric;

        this.sortColumnIndex = column;

     var newRows = new Array();
     for (j = 0; j < this.tbody[0].rows.length; j++) {
            newRows[j] = this.tbody[0].rows[j];
        }

        newRows.sort(sortfn);
		
		
		
		for (i=0;i<this.thead[0].rows[0].cells.length;i++){
			this.thead[0].rows[0].cells[i].innerText=this.thead[0].rows[0].cells[i].innerText.replace(/\u2191/,'');
			this.thead[0].rows[0].cells[i].innerText=this.thead[0].rows[0].cells[i].innerText.replace(/\u2193/,'');
		}
		var txt = this.thead[0].rows[0].cells[column].innerText;
        if (cell.getAttribute("sortdir") == 'down') {
            newRows.reverse();
            cell.setAttribute('sortdir','up');
            
            //txt=txt.replace(/\u2191/,'')
            this.thead[0].rows[0].cells[column].innerText=txt+'\u2193';
        } else {
            cell.setAttribute('sortdir','down');
            //txt=txt.replace(/\u2193/,'')
            this.thead[0].rows[0].cells[column].innerText=txt+'\u2191';
        }
        
        for (i=0;i<newRows.length;i++) {
            this.tbody[0].appendChild(newRows[i]);
        }

    }

    this.sortCaseInsensitive = function(a,b) {
        aa = thisObject.getInnerText(a.cells[thisObject.sortColumnIndex]).toLowerCase();
        bb = thisObject.getInnerText(b.cells[thisObject.sortColumnIndex]).toLowerCase();
        if (aa==bb) return 0;
        if (aa<bb) return -1;
        return 1;
    }

    this.sortDate = function(a,b) {
        aa = thisObject.getInnerText(a.cells[thisObject.sortColumnIndex]);
        bb = thisObject.getInnerText(b.cells[thisObject.sortColumnIndex]);
        var date1  = new Date(aa);
		var date2  = new Date(bb);

        //date1 = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
        //date2 = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);
        if (date1==date2) return 0;
        if (date1<date2) return -1;
        //if (aa==bb) return 0;
        //if (aa<bb) return -1;
        return 1;
    }

    this.sortNumeric = function(a,b) {
        aa = parseFloat(thisObject.getInnerText(a.cells[thisObject.sortColumnIndex]));
        if (isNaN(aa)) aa = 0;
        bb = parseFloat(thisObject.getInnerText(b.cells[thisObject.sortColumnIndex]));
        if (isNaN(bb)) bb = 0;
        return aa-bb;
    }

    // define variables
    var thisObject = this;
    var sortSection = this.thead;

    // constructor actions
    if (!(this.tbody && this.tbody[0].rows && this.tbody[0].rows.length > 0)) return;

    if (sortSection && sortSection[0].rows && sortSection[0].rows.length > 0) {
        var sortRow = sortSection[0].rows[0];
    } else {
        return;
    }

    for (var i=0; i<sortRow.cells.length; i++) {
        sortRow.cells[i].sTable = this;
        sortRow.cells[i].onclick = function () {
            this.sTable.sort(this);
            return false;
        }
    }

}



</script>

<div class="noprint">
	<center>
		<asp:Button ID="_btnExportTop" Text="Export To Word" runat="server" />&nbsp;<asp:Button
			ID="_btnPrintTop" runat="server" Text="Print" OnClientClick="window.print();return false;" /></center>
</div>
<div id="_divGanttChart" runat="server">
</div>
<div class="noprint">
	<center>
		<asp:Button ID="_btnExport" Text="Export To Word" runat="server" />&nbsp;<asp:Button
			ID="_btnPrint" runat="server" Text="Print" OnClientClick="window.print();return false;" /></center>
</div>

<script language="javascript" type="text/javascript">
	var t = new SortableTable(document.getElementById('tbl'), 5);
</script>

