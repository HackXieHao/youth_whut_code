// JavaScript Document
				$(function () { 
				var scrollTimer = null; 
				var delay = 4000; 
				//1.�������Ŵ���mouseout�¼���ÿ2s����scrollNews() 
				//2.�������Ŵ���mouseover�¼���ֹͣ����scrollNews() 
				//3.���μ���ҳ�津���������ŵ�mouseout�¼� 
				$('div.scrollNews').hover(function () { 
				clearInterval(scrollTimer); 
				}, function () { 
				scrollTimer = setInterval(function () { 
				scrollNews(); 
				}, delay); 
				}).triggerHandler('mouseout'); 
				}); 
				//�������� 
				function scrollNews() { 
				var $news = $('div.scrollNews>ul'); 
				var $lineHeight = $news.find('li:first').height(); 
				$news.animate({ 'marginTop': -$lineHeight + 'px' }, 600, function () { 
				$news.css({ margin: 0 }).find('li:first').appendTo($news); 
				}); 
				} 
