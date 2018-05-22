// JavaScript Document
$(function(){
            var tabs = $( "#tabsMenu" ).tabs();
                       var tabTemplate = "<li><a href='#{href}'>#{label}</a> <span class='fa fa-times-circle'>X</span></li>";
                $('#sidebar li').click(function(){
                       //��ȡtabs��a[href]��ֵ
                        var id="#tabs-"+this.id;
                        //tabs��ʼ��ʱ����һ��li,����Ҫ��1�����ʱindex�᷵��-1���ټ�1��Ϊ-2���ɸ���ʵ���������������ʵ������ͨ��Id��λ#id����li��λ�ã�Ȼ������active
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
                          //���ʱ���Ƿ���-1
                          tabs.tabs('option','active',index);
                       
                          }
                        // close icon: removing the tab on click
                        $( "#tabsMenu" ).on( "click",'span.fa-times-circle', function() {
                            var panelId = $( this ).closest( "li" ).remove().attr( "aria-controls" );
                            $( "#" + panelId ).remove();
                            tabs.tabs( "refresh" );
                        });
             
        })