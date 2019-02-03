(function () {
    $(function () {

        var _roleService = abp.services.app.settlement;
        var _$modal = $('#SettlementCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({
        });

        $('#RefreshButton').click(function () {
            refreshList();
        });

        $('.delete-settlement').click(function () {
            var settlementId = $(this).attr("data-settlement-id");
            var settlementName = $(this).attr('data-settlement-name');

            deleteSettlement(settlementId, settlementName);
        });

        $('.undo-settlement').click(function () {
            var settlementId = $(this).attr("data-settlement-id");
            var settlementName = $(this).attr('data-settlement-name');

            undoSettlement(settlementId, settlementName);
        });

        $('.edit-settlement').click(function (e) {
            var settlementId = $(this).attr("data-settlement-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Settlement/EditSettlement?settlementId=' + settlementId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#SettlementEditModal div.modal-content').html(content);
                },
                error: function (e)
                {

                }
            });
        });


        function refreshList() {
            location.reload(true);
        }

        function deleteSettlement(settlementId, settlementName) {

            var settlementId = settlementId;
            abp.message.confirm(
                "Remove exepnse entry '" + settlementName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var SettlementId = settlementId;

                        $.ajax({
                            url: abp.appPath + 'Settlement/DeleteSettlement?settlementId=' + settlementId,
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

        function undoSettlement(settlementId, settlementName) {

            var settlementId = settlementId;
            abp.message.confirm(
                "Undo deleted exepnse entry '" + settlementName + "'?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        // ajax call

                        //var SettlementId = settlementId;

                        $.ajax({
                            url: abp.appPath + 'Settlement/UndoSettlement?settlementId=' + settlementId,
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