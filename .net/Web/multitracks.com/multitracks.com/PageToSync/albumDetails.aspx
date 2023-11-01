<%@ Page Language="C#" AutoEventWireup="true" CodeFile="albumDetails.aspx.cs" Inherits="PageToSync_albumDetails" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <!-- set the viewport width and initial-scale on mobile devices -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />

    <!-- set the encoding of your site -->
    <meta charset="utf-8" />
    <title>MultiTracks.com</title>
    <!-- include the site stylesheet -->
    <link media="all" rel="stylesheet" href="./css/index.css" />
</head>
<p runat="server" id="errorMessage">
    <strong> This page is unavailable at the moment, please try again later.</strong>
</p>
<body id="items" runat="server">
    <asp:Repeater ID="rptArtist" runat="server" OnItemDataBound=" rptArtist_ItemDataBound">
        <ItemTemplate>
            <form>
                <noscript>
                    <div>Javascript must be enabled for the correct page display</div>
                </noscript>

                <!-- allow a user to go to the main content of the page -->
                <a class="accessibility" href="#main" tabindex="21">Skip to Content</a>

                <div class="wrapper mod-standard mod-gray">
                    <div class="details-banner">
                        <div class="details-banner--overlay"></div>
                        <div class="details-banner--hero">
                            <img class="details-banner--hero--img" src="<%# Eval("heroURL") %>"
                                srcset="<%# Eval("heroURL") %>, 
						<%# Eval("heroURL") %> 2x"
                                alt="<%# Eval("title") %>">
                        </div>
                        <div class="details-banner--info">
                            <a href="#" class="details-banner--info--box">
                                <img class="details-banner--info--box--img"
                                    src="<%# Eval("imageURL") %>"
                                    srcset="<%# Eval("imageURL") %>,
								 			<%# Eval("imageURL") %>"
                                    alt="alt">
                            </a>
                            <h1 class="details-banner--info--name"><a class="details-banner--info--name--link" href="#"><%# Eval("title") %></a></h1>
                        </div>
                    </div>

                    <nav class="discovery--nav">
                        <ul class="discovery--nav--list tab-filter--list u-no-scrollbar">
                            <li class="discovery--nav--list--item tab-filter--item">
                                <a class="tab-filter" href="<%# "artistDetails.aspx?id="+Eval("artistID")%>">Overview</a>
                            </li>
                            <li class="discovery--nav--list--item tab-filter--item">
                                <a class="tab-filter" href="<%# "songDetails.aspx?id="+Eval("artistID")%>">Songs</a>
                            </li>
                            <li class="discovery--nav--list--item tab-filter--item is-active">
                                <a class="tab-filter" href="#">Albums</a>
                            </li>
                        </ul>
                    </nav>


                    <div class="discovery--container u-container">
                        <main class="discovery--section">

                            <div class="discovery--space-saver">
                                <section class="standard--holder">
                                    <div class="discovery--grid-holder">
                                        <div class="ly-grid ly-grid-cranberries">
                                            <asp:Repeater ID="rptAlbums" runat="server">
                                                <ItemTemplate>
                                                    <div class="media-item">
                                                        <a class="media-item--img--link" href="#" tabindex="0">
                                                            <img class="media-item--img" alt="Reckless Love" src="<%# Eval("albumImageURL") %>" srcset="<%# Eval("albumImageURL") %>, <%# Eval("albumImageURL") %> 2x">
                                                            <span class="image-tag">Master</span>
                                                        </a>
                                                        <a class="media-item--title" href="#" tabindex="0"><%# Eval("albumTitle") %></a>
                                                        <a class="media-item--subtitle" href="#" tabindex="0"><%# Eval("artistTitles") %></a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </section>
                            </div>
                        </main>
                    </div>
                </div>
                <a class="accessibility" href="#wrapper" tabindex="20">Back to top</a>
            </form>
        </ItemTemplate>
    </asp:Repeater>
</body>
</html>
