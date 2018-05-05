$(document).ready(function () {
  getAll();

  $("button#tasks").on('click', function () {
    // if visible then hide else show
    $("table#completed-tasks").toggle();

    var visible = $("table#completed-tasks").is(':visible')
    if (visible) {
      $(this).text('hide completed tasks');
    } else {
      $(this).text('show completed tasks');
    }
  })
});


function getAll() {
  $.getJSON('/api/tasks').done(function (data) {
    // On success, 'data' contains a list of tasks.
    $.each(data, function (key, item) {
      // Add a list item for the task
      tableAddRow(item, item.Completed ? "completed" : "waiting");
    });
  });
}

function tableAddRow(item, tableName) {
  //var id = $(document.createElement('th')).append(item.Id);
  var checkbox = $(document.createElement('td')).append(checkboxCompleted(item));
  var title = $(document.createElement('td')).append(item.Title);
  var description = $(document.createElement('td')).append(item.Description);

  var row = $(document.createElement('tr')).append(checkbox, title, description);

  $("tbody#" + tableName).append(row);
}

function checkboxCompleted(item) {
  return $(document.createElement('input')).attr('type', 'checkbox').attr('name', 'task')
    .attr('onchange', 'toggleCheckbox(this)').attr('checked', item.Completed).val(item.Id);
}

function toggleCheckbox(element) {
  element.checked = !element.checked;
  console.log('task change completed');
}
