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
  });

  $("button#save").on('click', function () {
    console.log('will save');

    let formData = $("form").serialize();

    console.log(formData);

    $.ajax({
      type: 'POST',
      url: '/api/tasks/',
      data: formData,
      success: function (data) {
        // clear the form
        $('form').trigger("reset");
        resetTables();
      },
      error: function (data) {
        console.log(data);
      },
      contentType: "application/x-www-form-urlencoded",
    });
  });
});


function getAll() {
  $.getJSON('/api/tasks').done(function (data) {
    // On success, 'data' contains a list of tasks.
    $.each(data, function (key, item) {
      // skip item if deleted
      if (item.Deleted) { return true; }
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
  var destroyButton = $(document.createElement('td')).append(deleteButton(item));

  var row = $(document.createElement('tr')).append(checkbox, title, description, destroyButton);

  $("tbody#" + tableName).append(row);
}

function checkboxCompleted(item) {
  return $(document.createElement('input')).attr('type', 'checkbox').attr('name', 'task')
    .attr('onchange', 'toggleCheckbox(this)').attr('checked', item.Completed).val(item.Id)
    .attr('title', item.Title);
}

function toggleCheckbox(element) {
  let data = { "Completed": element.checked, "Id": element.value, "Title": element.title };
  $.ajax({
    type: 'PUT',
    url: '/api/tasks/' + element.value,
    contentType: 'application/json',
    data: JSON.stringify(data), // access in body
  }).done(function () {
    resetTables();
  }).fail(function (msg) {
    //console.log('FAIL');
  }).always(function (msg) {
    //console.log('ALWAYS');
  });
}

function resetTables() {
  $("tbody tr").remove();
  getAll();
}

function deleteButton(item) {
  var a = $(document.createElement('a')).attr('id', item.Id).attr('onclick', 'deleteItem(this)');
  var icon = $(document.createElement('i')).addClass('glyphicon glyphicon-trash');
  return a.append(icon);
}

function deleteItem(item) {
  var result = confirm("Are you sure?");
  if (result) {
    $.ajax({
      url: '/api/tasks/' + item.id,
      type: 'DELETE',
      success: function (result) {
        //console.log('Item deleted');
        $('a#' + item.id).closest('tr').remove();
      }
    });   
  } else {
    console.log('item not deleted');
  }
}