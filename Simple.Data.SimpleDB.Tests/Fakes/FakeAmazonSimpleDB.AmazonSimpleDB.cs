using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;

namespace Simple.Data.SimpleDB.Tests.Fakes
{
	partial class FakeAmazonSimpleDB : AmazonSimpleDB
	{
	    #region Implementation of IDisposable

	    void IDisposable.Dispose()
	    {
	        throw new NotImplementedException();
	    }

	    #endregion

	    #region Implementation of AmazonSimpleDB

	    IAsyncResult AmazonSimpleDB.BeginCreateDomain(CreateDomainRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    CreateDomainResponse AmazonSimpleDB.EndCreateDomain(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    CreateDomainResponse AmazonSimpleDB.CreateDomain(CreateDomainRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginListDomains(ListDomainsRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    ListDomainsResponse AmazonSimpleDB.EndListDomains(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    ListDomainsResponse AmazonSimpleDB.ListDomains(ListDomainsRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginDomainMetadata(DomainMetadataRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    DomainMetadataResponse AmazonSimpleDB.EndDomainMetadata(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    DomainMetadataResponse AmazonSimpleDB.DomainMetadata(DomainMetadataRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginDeleteDomain(DeleteDomainRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    DeleteDomainResponse AmazonSimpleDB.EndDeleteDomain(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    DeleteDomainResponse AmazonSimpleDB.DeleteDomain(DeleteDomainRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginPutAttributes(PutAttributesRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    PutAttributesResponse AmazonSimpleDB.EndPutAttributes(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginBatchPutAttributes(BatchPutAttributesRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    BatchPutAttributesResponse AmazonSimpleDB.EndBatchPutAttributes(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    BatchPutAttributesResponse AmazonSimpleDB.BatchPutAttributes(BatchPutAttributesRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginGetAttributes(GetAttributesRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    GetAttributesResponse AmazonSimpleDB.EndGetAttributes(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    GetAttributesResponse AmazonSimpleDB.GetAttributes(GetAttributesRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginDeleteAttributes(DeleteAttributesRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    DeleteAttributesResponse AmazonSimpleDB.EndDeleteAttributes(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    DeleteAttributesResponse AmazonSimpleDB.DeleteAttributes(DeleteAttributesRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginBatchDeleteAttributes(BatchDeleteAttributesRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    BatchDeleteAttributesResponse AmazonSimpleDB.EndBatchDeleteAttributes(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    BatchDeleteAttributesResponse AmazonSimpleDB.BatchDeleteAttributes(BatchDeleteAttributesRequest request)
	    {
	        throw new NotImplementedException();
	    }

	    IAsyncResult AmazonSimpleDB.BeginSelect(SelectRequest request, AsyncCallback callback, object state)
	    {
	        throw new NotImplementedException();
	    }

	    SelectResponse AmazonSimpleDB.EndSelect(IAsyncResult asyncResult)
	    {
	        throw new NotImplementedException();
	    }

	    #endregion
	}
}
