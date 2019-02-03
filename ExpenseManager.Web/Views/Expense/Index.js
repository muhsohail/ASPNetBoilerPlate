(function () {
    $(function () {

        var _roleService = abp.services.app.expense;
        var _$modal = $('#ExpenseCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-expense').click(function () {
            var expenseId = $(this).attr("data-expense-id");
            var expenseName = $(this).attr('data-expense-name');

            deleteExpense(expenseId, expenseName);
        });

        $('.undo-expense').click(function () {
            var expenseId = $(this).attr("data-expense-id");
            var expenseName = $(this).attr('data-expense-name');

            undoExpense(expenseId, expenseName);
        });

        $('.edit-expense').click(function (e) {
            var expenseId = $(this).attr("data-expense-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Expense/EditExpense?expenseId=' + expenseId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#ExpenseEditModal div.modal-content').html(content);
                },
                error: function (e)
                {

                }
            });
        });


        function refreshList() {
            location.reload(true);
        }

        function deleteExpense(expenseId, expenseName) {

            var expenseId = expenseId;
            abp.message.confirm(
                "Remove exepnse entry '" + expenseName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var expenseId = expenseId;

                        $.ajax({
                            url: abp.appPath + 'Expense/DeleteExpense?expenseId=' + expenseId,
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

        function undoExpense(expenseId, expenseName) {

            var expenseId = expenseId;
            abp.message.confirm(
                "Undo deleted exepnse entry '" + expenseName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var expenseId = expenseId;

                        $.ajax({
                            url: abp.appPath + 'Expense/UndoExpense?expenseId=' + expenseId,
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