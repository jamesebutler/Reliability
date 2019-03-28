<%@ Page Language="VB" MasterPageFile="~/RI.master" AutoEventWireup="false" CodeFile="MOCQuestions.aspx.vb"
    Inherits="MOC_Questions" Title="Management of Change" Trace="false" EnableViewState="true"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/RI.master" %>

<asp:Content ID="_contentMain" ContentPlaceHolderID="_cphMain" runat="Server">

    <link href="../../App_Themes/Main.css" rel=" stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../Content/handsontable/handsontable.full.min.css" />
    <script src="../../scripts/handsontable/handsontable.full.min.js" type="text/javascript"></script>
    
    <asp:Label runat="server" ID="_lblMainHeading"></asp:Label><br />
    <asp:HiddenField runat="server" ID="_hfUserID" />
    <asp:HiddenField runat="server" ID="_hfAuthLevel" />
    <asp:HiddenField runat="server" ID="_hfDefaultSiteID" />
    <asp:HiddenField runat="server" ID="_hfDefaultBusiness" />
    

    <table style="display: block; width: 600pt">
        <tr style="width: auto">
            <td style="width: 250px">
                <asp:Label ID="_lblBustype" runat="server" Text='<%$ RIResources:Shared,Business %>'></asp:Label><br />
                <select id="_ddlBustype" name="_ddlBustype" onchange="BusTypeChange();"></select>
            </td>
            <td style="width: 250px">
                <asp:Label ID="_lblDivision" runat="server" Text='<%$ RIResources:Shared,Division %>'></asp:Label><br />
                <select id="_ddlDivision" name="_ddlDivision" onchange="DivisionChange();"></select>
            </td>
            <td style="width: 250px">
                <asp:Label ID="_lblFacility" runat="server" Text='<%$ RIResources:Shared,Facility %>'></asp:Label><br />
                <select id="_ddlSites" name="_ddlSites"></select>
            </td>
        </tr>

<%--        <tr>
            <td style="width: 226px">
                <asp:DropDownList ID="_ddlFacility" runat="server" AutoPostBack="false" Visible="false">
                    <asp:ListItem Value="ALL" Text="Enterprise"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>

        <tr>
            <td colspan="3">
                <asp:RadioButtonList ID="_rblMaintType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Classification" Text="<%$RIResources:Shared,Classification %>" Selected></asp:ListItem>
                    <asp:ListItem Value="Category" Text="<%$RIResources:Shared,Category %>"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>

    <table>
        <tr style="width: 100pt">
            <td style="width: 744px">
                <div id="Classification">
                    <asp:Label ID="_lblClassification" runat="server" Text="<%$RIResources:Shared,Classification %>"></asp:Label><br />
                    <asp:DropDownList ID="_ddlClassification" runat="server" ClientIDMode="Static" />
                </div>
                <div id="Category" style="display: none">
                    <asp:Label ID="_lblCategory" runat="server" Text="<%$RIResources:Shared,Category %>"></asp:Label><br />
                    <asp:DropDownList ID="_ddlCategory" runat="server" ClientIDMode="Static" />
                </div>
            </td>
        </tr>

        <tr>
            <td style="width: 744px">
                <div id="divbuttons" style="display: block">
                    <br />
                    <input id="btnsearch" type="button" value="Search/Add" onclick="showGetResult(); showDivResult();" style="width: 100px; display: none;" />
                    <input id="btnUpdate" type="button" value="Save" onclick="saveResults();" style="width: 52px; display: none;" />
                    <br />
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                <div id="divbusy" style="display: none">
                    <img src="../../Images/indicator_medium.gif" />
                </div>
                <br />
                <div id="_divedit" style="display: none">
                    <label id="_lbledit" class="Label"></label>
                </div>
                <div id="_questionGrid" class="handsontable"></div>
            </td>
        </tr>

       <tr>
            <td colspan="4">
                <div id="_divgroup" style="display: none">
                    <br />
                    <label id="_lbldiv" class="Label"></label>
                    <br />
                </div>
                <div id="_divquestionGrid" class="handsontable"></div>
            </td>

        </tr>

    </table>

    <script type="text/javascript">
        var userprofile = [];
        var hot;
        var hot2;
        var username = $("#<%=_hfUserID.ClientID%>").val();

        $(document).ready(function () {
            var siteid = $("#<%=_hfDefaultSiteID.ClientID%>").val();

            //    function hideResults() {
            //        $("#btnUpdate").hide();
            //        $("#_divgroup").hide();
            //        $("#_divquestionGrid").hide();
            //        $("#_divedit").hide();
            //        $("#_questionGrid").hide();
            //    };

            <%--var siteid = $("#<%=_hfDefaultSiteID.ClientID%>").val();

            var obj1 = {};
            obj1.siteid = "GT";

            $.ajax({
                type: "POST",
                url: "./../MOCWS.asmx/GetFacilityInfo",
                data: JSON.stringify(obj1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    part = data.d;
                    //var dataObject = JSON.parse(part);
                    //defaultbustype = this['BUSTYPE'];
                    //userinfo = data;
                    //alert(data);
                    //defaultbustype = userinfo.bustype;
                    //defaultdivision = userinfo.division;
                },
                error: function () {
                    alert(obj1);
                    alert("Failed to load Division");
                }
            });--%>

            var obj = {};
            //obj.siteid = siteid
            obj.username = username;

            $.ajax({
                type: "POST",
                url: "../../MOCWS.asmx/GetBustype",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var dataObject = JSON.parse(data.d);

                    $("#_ddlBustype").get(0).options.length = 0;
                    $("#_ddlBustype").get(0).options[0] = new Option("Select Business", "-1");

                    $.each(dataObject, function (index, item) {
                        $("#_ddlBustype").append($("<option></option>").val(this['BUSTYPE']).html(this['BUSNAME']));
                    });
                    //$('#_ddlBustype option[value=PM]').attr('selected', true);
                    //$("#_ddlBustype").change();

                },
                error: function () {
                    alert("Failed to load Business Type");
                }
            });


            $('#<%=_rblMaintType.ClientID%>').change(function () {
                var op1 = $('#<%=_rblMaintType.ClientID%> input:checked').val();

                if (op1 == 'Classification') {
                    $('#Classification').slideDown();
                    $('#Category').slideUp();
                    //$("#_questionGrid").hide();
                    //$("#_divquestionGrid").hide();
                    //$("#_divgroup").hide();
                    //$("#_lbledit").hide();
                }
                else {
                    $('#Classification').slideUp();
                    $('#Category').slideDown();
                    //$("#_questionGrid").hide();
                    //$("#_divquestionGrid").hide();
                    //$("#_divgroup").hide();
                    //$("#_lbledit").hide();
                }
                $('#btnsearch').click();
            });

            $("#_ddlClassification").on("change", function () {
                $('#btnsearch').click();
                //                hideResults();
            });

            $("#_ddlCategory").on("change", function () {
                $('#btnsearch').click();
                //              hideResults();
            });

            function hideResults() {
                $("#btnUpdate").hide();
                $("#_divgroup").hide();
                $("#_divquestionGrid").hide();
                $("#_divedit").hide();
                $("#_questionGrid").hide();
            };
        });


        function showGetResult() {
            //$("#btnsearch").on("click", function () {
            var container = document.getElementById('_questionGrid');
            //var divcontainer = document.getElementById('_divquestionGrid');

            var bustype = $("#_ddlBustype option:selected").val();
            var bustypename = $("#_ddlBustype option:selected").html();
            var division = $("#_ddlDivision option:selected").val();
            var divisionname = $("#_ddlDivision option:selected").html();
            var siteid = $("#_ddlSites option:selected").val();
            var sitename = $("#_ddlSites option:selected").html();

            var obj = {};

            obj.siteID = siteid;
            obj.division = division;
            obj.bustype = bustype;


            var op1 = $('#<%=_rblMaintType.ClientID%> input:checked').val();
            if (op1 == 'Classification') {
                var classid = $("#<%=_ddlClassification.ClientID%> option:selected").val();
                var classname = $("#<%=_ddlClassification.ClientID%> option:selected").html();
                obj.classID = classid;
                obj.type = 'classification';
            } else {
                var classid = $("#<%=_ddlCategory.ClientID%> option:selected").val();
                var classname = $("#<%=_ddlCategory.ClientID%> option:selected").html();
                obj.classID = classid;
                obj.type = 'category';
            };

            $.ajax({
                type: "POST",
                url: "../../MOCWS.asmx/GetMOCQuestions",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    $("#divbusy").show();
                    //if (hot != null) {
                    //    hot.destroy();
                    //};
                },
  //              beforeChange: function  B(changes, source){
  //  if (source !== 'paste'){
  //    return;
  //}
  //          changes.forEach(function(change){
  //              if(change[3] === 'true'){
  //                  change[3] = true;
  //              } else if ( change[3] === 'N'){
  //                  change[3] = false;
  //              }
  //          });
  //      },
                async: true,
                success: function (data) {
                    //alert(data.d);
                    $("#_questionGrid").show();

                    part = data.d;
                    var dataObject = JSON.parse(part);

                    //alert(data);
                    if (hot != null) {
                        hot.destroy();
                    }
                    hot = new Handsontable(container, {
                        data: dataObject,
                        columns: [
                            {
                                data: 'TITLE',
                                type: 'text'

                            },
                            //{
                              //  data: 'SUBTITLE',
                              //  type: 'text'

                            //},
                            {
                                data: 'QUESTIONTYPE',
                                type: 'dropdown',
                                source: ['Y/N', 'Text']
                            },
                        {
                            data: 'INACTIVEFLAG',
                            type: 'checkbox',
                            checkedTemplate: 'Y',
                            uncheckedTemplate: null

                        },
                        {
                            data: 'MOCQUESTION_SEQID',
                            type: 'text',
                            editor: false
                        }
                        ],
                        hiddenColumns: {
                            columns: [3],
                            indicators: true
                        },
                        colWidths: [800, 75, 75, 5],
                        colHeaders: ["Title", "Type", "Inactive", "ID"],
                        columnSorting: false,
                        contextMenu: ['row_above', 'row_below'],
                        autoWrapRow: true,
                        rowHeaders: true,
                        manualRowMove: true,
                        minSpareRows: 1,

                    });
                    $('th').css('background-color', 'gray');
                    $("#divbusy").hide();
                    $("#btnUpdate").show();
                    $("#_lbledit").empty();
                    $("#_lbledit").append('Add/Update Questions For ' + bustypename + '/' + divisionname + '/' + sitename + '-' + classname);
                    $("#_lbledit").show();
                    $("#_divedit").show();

                    $("#_questionGrid").show();
                    hot.render;

                },

                error: function (error) {
                    $("#divbusy").hide();
                    $("#_questionGrid").hide();
                    alert("Failed to load questions: " + status);
                }
            });
        };


        function showDivResult() {
            $("#_lbldiv").empty();
            var bustype = $("#_ddlBustype option:selected").val();
            var bustypename = $("#_ddlBustype option:selected").html();
            var division = $("#_ddlDivision option:selected").val();
            var siteid = $("#_ddlSites option:selected").val();

            if (division != "AL") {
                var divcontainer = document.getElementById('_divquestionGrid');

                $("#_lbldiv").append('Business and Division Questions');

                var obj = {};
                obj.siteID = siteid;
                obj.division = division;
                obj.bustype = bustype;

                var op1 = $('#<%=_rblMaintType.ClientID%> input:checked').val();
                if (op1 == 'Classification') {
                    var classid = $("#<%=_ddlClassification.ClientID%> option:selected").val();
                    obj.classID = classid;
                    obj.type = 'classification';
                } else {
                    var classid = $("#<%=_ddlCategory.ClientID%> option:selected").val();
                    obj.classID = classid;
                    obj.type = 'category';
                };

                $.ajax({
                    type: "POST",
                    url: "../../MOCWS.asmx/GetDivMOCQuestions",
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#divbusy").show();
                    },
                    async: true,
                    success: function (data) {
                        //alert(data.d);
                        if (data.d == '[]') {
                            $("#_divgroup").hide();
                            $("#_divquestionGrid").hide();
                        }
                        else {
                            $("#_divgroup").show();

                            part = data.d;
                            var dataObject = JSON.parse(part);

                            //alert(data);
                            if (hot2 != null) {
                                hot2.destroy();
                            };

                            hot2 = new Handsontable(divcontainer, {
                                data: dataObject,
                                columns: [
                                    {
                                        data: 'GROUPNAME',
                                        type: 'text',
                                        editor: false
                                    },
                                    {
                                        data: 'TITLE',
                                        type: 'text',
                                        editor: false
                                    },
                                    {
                                        data: 'QUESTIONTYPE',
                                        type: 'dropdown',
                                        source: ['Y/N', 'Text'],
                                        editor: false
                                    }
                                ],
                                colWidths: [200, 800, 75],
                                colHeaders: ["Business/Division", "Title", "Type"],
                                columnSorting: true,
                                autoWrapRow: true,
                            })
                            $('ht').css('background-color', 'gray');
                            $("#divbusy").hide();
                            $("#_divgroup").show();
                            $("#_divquestionGrid").show();
                            hot2.render;
                        }

                    },

                    error: function (error) {
                        $("#divbusy").hide();
                        $("#_divgroup").hide();
                        $("#_divquestionGrid").hide();
                        alert("Failed to load questions: " + status);
                    }
                });
            }
        };


        function BusTypeChange() {
            $("#btnUpdate").hide();
            $("#_questionGrid").hide();
            $("#_divquestionGrid").hide();
            $("#_divgroup").hide();
            $("#_ddlSites").empty();
            var userid = $("#<%=_hfUserID.ClientID%>").val();
            var bustype = $("#_ddlBustype option:selected").val();
            var obj = {};
            obj.bustype = bustype;
            obj.username = username;

            $.ajax({
                type: "POST",
                url: "../../MOCWS.asmx/GetDivision",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#_ddlDivision").empty();
                    var dataObject = JSON.parse(data.d);

                    //$("#_ddlDivision").get(0).options.length = 0;
                    //$("#_ddlDivision").get(0).options[0] = new Option("Select Division", "-1");

                    $.each(dataObject, function (index, item) {
                        $("#_ddlDivision").append($("<option></option>").val(this['DIVISION']).html(this['DIVISION']));
                    });
                    //$('#_ddlDivision').trigger("onchange");
                    var userid = $("#<%=_hfAuthLevel.ClientID%>").val();
                    if (userid == 'MTTSUPPORT') {
                        $('#_ddlDivision').prepend($('<option></option>').val("AL").html('All'));
                        $('#_ddlSites').prepend($('<option></option>').val("AL").html('All'));
                        if (bustype != '-1') {
                        $('#btnsearch').click();
                        $("#btnUpdate").show();
                        } else {
                            $("#_divedit").hide();
                        }
                    }
                    else {
                        $('#_ddlDivision').prepend($('<option></option>').val("").html('Select Division'));
                        $("#_divedit").hide();
                        $("#btnUpdate").hide();
                    };
                    //$('#_ddlDivision option[value=NAP]').attr('selected', true);
                    //$("#_ddlDivision").change();
                },
                error: function () {
                    alert("Failed to load Division");
                }
            });
        };
        //);

        // Site drop down list value changed so get sites for the selected business.

        function DivisionChange() {
            $("#_ddlSites").empty();
            $("#btnUpdate").hide();
            $("#_questionGrid").hide();
            $("#_divquestionGrid").hide();
            $("#_divgroup").hide();
            $("#_divedit").hide();
            var obj = {};
            obj.division = $("#_ddlDivision option:selected").val();
            obj.username = username;
            $.ajax({
                type: 'POST',
                url: "../../MOCWS.asmx/GetFacility",
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                data: JSON.stringify(obj),
                success: function (data) {
                    $("#_ddlSites").empty();
                    var dataObject = JSON.parse(data.d);

                    $.each(dataObject, function (index, item) {
                        $("#_ddlSites").append($("<option></option>").val(this['SITEID']).html(this['SITENAME']));
                    });

                    var userid = $("#<%=_hfAuthLevel.ClientID%>").val();
                    if (userid == 'MTTSUPPORT') {
                        $('#_ddlSites').prepend($('<option></option>').val("AL").html('All'));
                        $("#btnUpdate").show();
                        $('#btnsearch').click();
                    }
                    else {
                        $('#_ddlSites').prepend($('<option></option>').val("").html('Select Facility'));
                        $("#btnUpdate").hide();
                    };
                    //$('#_ddlSites option[value="GT"]').attr('selected', true)
                },
                error: function () {
                    alert("Failed to load Facilities");
                }
            });
        };
        //);

        $("#_ddlSites").on("change", function () {
            var facilityid = $("#_ddlSites option:selected").val();
            if (facilityid != "") {
                $("#btnUpdate").show();
                //$("#divbuttons").show();
                $("#divbusy").hide();
                $('#btnsearch').click();

            }
            else {
                $("#divbuttons").show();
                $("#btnUpdate").hide();
            };
            $("#_questionGrid").hide();
            $("#_divquestionGrid").hide();
            $("#_divgroup").hide();
            $("#_divedit").hide();


        });


        //$.ajax({
        //    type: "POST",
        //    url: "../../RIMOCSharedWS.asmx/GetClassification",
        //    data: "{}",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (data) {
        //        var dataObject = JSON.parse(data.d);

        //        //$("#_ddlClass").get(0).options.length = 0;
        //        $("#_ddlClass").get(0).options[0] = new Option("Select Classification", "-1");

        //        $.each(dataObject, function (index, item) {
        //            $("#_ddlClass").append($("<option></option>").val(this['MOCCLASSIFICATION_SEQ_ID']).html(this['MOCCLASSIFICATION']));
        //        });
        //    },
        //    error: function () {
        //        alert("Failed to load classification");
        //    }
        //});
        //;

        $(document).ready(function () {

            

        });

        function saveResults() {
            $("#divbusy").show();
            var bustype = $("#_ddlBustype option:selected").val();
            var division = $("#_ddlDivision option:selected").val();
            var siteid = $("#_ddlSites option:selected").val();

            var userid = $("#<%=_hfUserID.ClientID%>").val();
            var griddata = hot.getData()

            var op1 = $('#<%=_rblMaintType.ClientID%> input:checked').val();
            if (op1 == 'Classification') {
                var classid = $("#<%=_ddlClassification.ClientID%> option:selected").val();
                var obj = { type: 'classification', classseq: classid, bustype: bustype, division: division, site: siteid, username: userid, data: JSON.stringify(griddata) };
            } else {
                var classid = $("#<%=_ddlCategory.ClientID%> option:selected").val();
                var obj = { type: 'category', classseq: classid, bustype: bustype, division: division, site: siteid, username: userid, data: JSON.stringify(griddata) };
            }
            //var obj = { classseq: $("#<%=_ddlClassification.ClientID%> option:selected").val(), site: $("#<%=_ddlClassification.ClientID%> option:selected").val()};

            $.ajax({
                type: "POST",
                url: "../../MOCWS.asmx/SaveQuestions",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(obj),
                success: function (r) {
                    $("#btnUpdate").show();
                    $("#btnsearch")[0].onclick();
                    $("#divbusy").hide();
                    alert("Data Saved");
                },
                error: function (r) {
                    $("#divbusy").hide();
                    alert("Failed to save");
                }
            });
        };

    </script>

</asp:Content>
