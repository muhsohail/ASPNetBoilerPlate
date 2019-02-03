//(function ($) {

//    var _roleservice = abp.services.app.role;
//    var _$modal = $('#expenseeditmodal');
//    var _$form = $('form[name=roeditexpenseform]');

//    function save() {

//        if (!_$form.valid()) {
//            return;
//        }

//        var role = _$form.serializeformtoobject(); //serializeformtoobject is defined in main.js
//        role.permissions = [];
//        var _$permissioncheckboxes = $("input[name='permission']:checked:visible");
//        if (_$permissioncheckboxes) {
//            for (var permissionindex = 0; permissionindex < _$permissioncheckboxes.length; permissionindex++) {
//                var _$permissioncheckbox = $(_$permissioncheckboxes[permissionindex]);
//                role.permissions.push(_$permissioncheckbox.val());
//            }
//        }

//        abp.ui.setbusy(_$form);
//        _roleservice.update(role).done(function () {
//            _$modal.modal('hide');
//            location.reload(true); //reload page to see edited role!
//        }).always(function () {
//            abp.ui.clearbusy(_$modal);
//        });
//    }

//    //handle save button click
//    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
//        e.preventdefault();
//        save();
//    });

//    //handle enter key
//    _$form.find('input').on('keypress', function (e) {
//        if (e.which === 13) {
//            e.preventdefault();
//            save();
//        }
//    });

//    $.adminbsb.input.activate(_$form);

//    _$modal.on('shown.bs.modal', function () {
//        _$form.find('input[type=text]:first').focus();
//    });
//})(jquery);