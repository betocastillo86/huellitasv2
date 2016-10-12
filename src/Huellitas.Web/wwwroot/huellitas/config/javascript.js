String.prototype.queryToJson = function () {
    var query = this.toString();
    var arr = query.split("&");
    result = {};
    for (i = 0; i < arr.length; i++) {
        k = arr[i].split('=');
        var value = k[1] || '';
        result[k[0]] = !isNaN(value) ? parseInt(value) : value;
    }
    console.log(result);
    return result;
};