window.sf = window.sf || {};

(function(sf) {

    "use strict";

    sf.search = {
        searchApiFormat: "/api/search/get?searchTerm={0}&skip={1}&take={2}",
        element: null,
        results: [],
        searchTerm: null,
        init: function() {
            this.initExtensions();
            this.initEvents();
            this.element = $(".results");
        },
        initExtensions: function() {
            //String format method
            if (!String.prototype.format) {
                String.prototype.format = function() {
                    var str = this.toString();
                    if (!arguments.length)
                        return str;
                    var args = typeof arguments[0],
                        args = (("string" == args || "number" == args) ? arguments : arguments[0]);
                    for (var arg in args)
                        str = str.replace(RegExp("\\{" + arg + "\\}", "gi"), args[arg]);
                    return str;
                }
            }
        },
        initEvents: function() {
            var me = this;

            $("form").submit(function(event) {
                event.preventDefault();
                var term = $("#inputSearch").val();
                if (term !== '') {
                    me.searchContent(term);
                }
            });
        },
        searchContent: function(term, skip, take) {
            var me = this,
                dfd = $.Deferred();

            skip = skip || 0;
            take = take || 10;

            var apiUrl = this.searchApiFormat.format(term, skip, take);

            $.ajax({
                type: 'GET',
                url: apiUrl
            }).done(function(data) {
                dfd.resolve();
                console.log(data);

                me.setContent(term, data);

            }).fail(function(jqXHR, textStatus) {
                dfd.reject();
            });

            return dfd.promise();
        },
        setContent: function(term, data) {
            var me = this;
            me.results = data.results;

            me.results.forEach(function(item) {
                me.element.append("<li class='list-group-item'><h2><a href='{0}'>{1}</a></h2></li>".format(item.link, item.title));
            });
        }
    };

    sf.search.init();

}(sf));



