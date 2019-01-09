$("#OrderTable").DataTable({
    "aoColumnDefs": [
        {
            "bSortable": false, "aTargets": [7]
        }
    ],
    "language": {
        "lengthMenu": "Покажи _MENU_ записки на страницата",
        "zeroRecords": "Няма намерени записки",
        "info": "показване на страница _PAGE_ от _PAGES_",
        "infoEmpty": "Няма записки",
        "infoFiltered": "(филтрирани от _MAX_ записки)",
        "search": "Потърси",
        "processing": "изпълняване...",
        "paginate": {
            "first": "Първи",
            "last": "Последен",
            "next": "Следващ",
            "previous": "Предишен"
        }
    }
});