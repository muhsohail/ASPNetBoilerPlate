(function () {
    $(function () {

        var _roleService = abp.services.app.personnelexpense;
        var _$modal = $('#PersonnelExpenseCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-personnelexpense').click(function () {
            var personnelexpenseId = $(this).attr("data-personnelexpense-id");
            var personnelexpenseName = $(this).attr('data-personnelexpense-name');

            deleteExpense(personnelexpenseId, personnelexpenseName);
        });


        $('.edit-personnelexpense').click(function (e) {
            var expenseId = $(this).attr("data-personnelexpense-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'PersonnelExpense/EditExpense?expenseId=' + expenseId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#PersonnelExpenseEditModal div.modal-content').html(content);
                },
                error: function (e)
                {

                }
            });
        });


        function refreshList() {
            location.reload(true);
        }

        function deleteExpense(personnelexpenseId, personnelexpenseName) {

            var personnelexpenseId = personnelexpenseId;
            abp.message.confirm(
                "Remove personnel exepnse entry '" + personnelexpenseName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var expenseId = expenseId;

                        $.ajax({
                            url: abp.appPath + 'PersonnelExpense/DeleteExpense?personnelexpenseId=' + personnelexpenseId,
                            type: 'POST',
                            contentType: 'application/html',
                            success: function (content) {
                                refreshList();
                            },
                            error: function (e) { }
                        });



                    }
                }
            );
        }
    });
})();


                        //_roleService.delete({
                        //    id: roleId
                        //}).done(function () {
                        //    refreshList();
                        //});