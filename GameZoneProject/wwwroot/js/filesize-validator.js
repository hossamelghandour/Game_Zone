$.Validator.addMethod('filesize', function (value, element, param) {
    var isvalid = this.optional(element) || element.files[0].size <= param

    return isvalid;
});
