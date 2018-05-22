// JavaScript Document
$(function(){
            var tabs = $( "#tabsMenu" ).tabs();
                       var tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='fa fa-times-circle'>X</span></li>";
                $('#sidebar li').click(function(){
                       //获取tabs下a[href]的值
                        var id="#tabs-"+this.id;
                        //tabs初始化时就有一个li,所以要减1，添加时index会返回-1，再减1变为-2，可根据实际情况而定。这里实际上是通过Id定位#id所在li的位置，然后设置active
                        var index=$("#tabsMenu").find(id).index()-1;
                         $( "#tabsMenu" ).tabs('option','active',index);
                          if(index==-2){
                             addTab(this.innerText,this.id);
                          }
                         
                });
                 
                function addTab(tabTitle,id) {
                        var label = tabTitle,
                            id = "tabs-" + id,
                            li = $( tabTemplate.replace( /#\{href\}/g, "#" + id ).replace( /#\{label\}/g, label ) );
                           var tabContentHtml = $("."+id).html();
                            var existing=tabs.find("[id='"+id+"']");
                            if(existing.length==0){
                                 tabs.find( ".ui-tabs-nav" ).append( li );
                                tabs.append( "<div id='" + id + "'><p>" + tabContentHtml + "</p></div>" );
                                tabs.tabs( "refresh" );
                            }
                             
                          var index=tabs.find('.ui-tabs-nav li').index(existing);
                          //添加时总是返回-1
                          tabs.tabs('option','active',index);
                       
                          }
                        // close icon: removing the tab on click
                        $( "#tabsMenu" ).on( "click",'span.fa-times-circle', function() {
                            var panelId = $( this ).closest( "li" ).remove().attr( "aria-controls" );
                            $( "#" + panelId ).remove();
                            tabs.tabs( "refresh" );
                        });
             
        })