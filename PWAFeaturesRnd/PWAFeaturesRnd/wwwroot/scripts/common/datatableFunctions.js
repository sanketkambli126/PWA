import moment from "moment";

export function GetCellData(label, data) {
	return '<label>' + label + '</label> <br />' + data;
}

export function GetExportCellData(label, data) {
	return '<label>' + label + '</label><br />' + GetExportData(data);
}

export function GetExportData(data) {
	return '<span class="export-Data">' + data + '</span>';
}

export function GetExportDataForHiddenElement(data) {
	return '<span class="export-Data d-none">' + data + '</span>';
}

export function GetFormattedDecimal(type, label, data, decimalPlaces, defaultValue) {
	var formattedDecimal = defaultValue;
	if (data != null && data != '' && data != 'undefined') {
		formattedDecimal = Number(parseFloat(data).toFixed(decimalPlaces)).toLocaleString('en',
			{
				minimumFractionDigits: decimalPlaces,
				maximumFractionDigits: decimalPlaces
			});
	}
	if (type === "display") {
		return GetCellData(label, formattedDecimal);
	}
	return formattedDecimal;
}

export function GetExportFormattedDecimal(type, label, data, decimalPlaces, defaultValue) {
	var formattedDecimal = defaultValue;
	if (data != null && data != '' && data != 'undefined') {
		formattedDecimal = Number(parseFloat(data).toFixed(decimalPlaces)).toLocaleString('en',
			{
				minimumFractionDigits: decimalPlaces,
				maximumFractionDigits: decimalPlaces
			});
	}
	if (type === "display") {
		return GetExportCellData(label, formattedDecimal);
	}
	return formattedDecimal;
}

export function GetFormattedDate(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != "" && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("DD MMM YYYY");
	}
	if (type === "display") {
		return GetCellData(label, formattedDate);
	}
	return date;
}

export function GetExportFormattedDate(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != "" && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("DD MMM YYYY");
	}
	if (type === "display") {
		return GetExportCellData(label, formattedDate);
	}
	return date;
}

export function GetFormattedDateTime(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != '' && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("DD MMM YYYY HH:mm");
	}
	if (type === "display") {
		return GetCellData(label, formattedDate);
	}
	return date;
}

export function GetFormattedTimeFromDate(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != '' && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("HH:mm");
	}
	if (type === "display") {
		return GetCellData(label, formattedDate);
	}
	return date;
}

export function GetExportFormattedDateTime(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != '' && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("DD MMM YYYY HH:mm");
	}
	if (type === "display") {
		return GetExportCellData(label, formattedDate);
	}
	return date;
}

export function GetFormattedDateTimeAMPMFormat(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != '' && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("lll");
	}
	if (type === "display") {
		return GetCellData(label, formattedDate);
	}
	return date;
}

export function GetExportFormattedDateTimeAMPMFormat(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != '' && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("lll");
	}
	if (type === "display") {
		return GetExportCellData(label, formattedDate);
	}
	return date;
}

export function GetFormattedDateTimeWithSeconds(type, label, data) {
	var date = "";
	var formattedDate = "";
	if (data != null && data != '' && data != undefined) {
		date = new Date(data);
		formattedDate = moment(date).format("DD MMM YYYY HH:mm:ss");
	}
	if (type === "display") {
		return GetCellData(label, formattedDate);
	}
	return date;
}


//to get customised header for export to excel functionality
export function CustomizedExcelHeader(xlsx, rowCount) {
	var sheet = xlsx.xl.worksheets['sheet1.xml'];
	$('row c[r*="2"]', sheet).attr('s', '55'); //Wrap test style set to 2nd row. Need to set \n for new lilne in messageTop.

	// height set to 2nd row. need to change the height as per the content in header.
	$('row* ', sheet).each(function (index) {
		if (index == 1) {
			$(this).attr('ht', (rowCount * 15)); //rowCount * 15 -> height that is created
			$(this).attr('customHeight', 1);
		}
	});
}

export function GetFormattedDecimalKPI(type, label, data, decimalPlaces, defaultValue, classSelector) {
	var formattedDecimal = defaultValue;
	if (data != null && data != '' && data != 'undefined') {
		formattedDecimal = Number(parseFloat(data).toFixed(decimalPlaces)).toLocaleString('en',
			{
				minimumFractionDigits: decimalPlaces,
				maximumFractionDigits: decimalPlaces
			});
	}
	if (type === "display") {
		return GetCellData(label, '<span class="' + classSelector + '">' + formattedDecimal + '</span>');
	}
	return formattedDecimal;
}

export function GetExportFormattedDecimalKPI(type, label, data, decimalPlaces, defaultValue, classSelector) {
	var formattedDecimal = defaultValue;
	if (data != null && data != '' && data != 'undefined') {
		formattedDecimal = Number(parseFloat(data).toFixed(decimalPlaces)).toLocaleString('en',
			{
				minimumFractionDigits: decimalPlaces,
				maximumFractionDigits: decimalPlaces
			});
	}
	if (type === "display") {
		return GetExportCellData(label, '<span class="' + classSelector + '">' + formattedDecimal + '</span>');
	}
	return formattedDecimal;
}
