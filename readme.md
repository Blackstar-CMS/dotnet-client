Blackstar CMS: .NET Client
==========================

[![Build status](https://ci.appveyor.com/api/projects/status/2dtum4n4pff377wh/branch/master?svg=true)](https://ci.appveyor.com/project/liammclennan/dotnet-client/branch/master)

[Blackstar CMS](http://blackstarcms.net) is an API-first, headless CMS, built for application developers.

Put managed content in your custom web application, and provide a delightful content management experience for application administrators.

This project is the .NET client for Blackstar CMS. 

Usage
-----

See the [Client Tests](https://github.com/Blackstar-CMS/dotnet-client/blob/master/test/Blackstar.Tests/ClientTests.cs) source. 

It is expected that most API requests will use the `GetByTagsAsync` method. For example, you might use a tag to identify all the content on a particular page, and then query by that tag.
