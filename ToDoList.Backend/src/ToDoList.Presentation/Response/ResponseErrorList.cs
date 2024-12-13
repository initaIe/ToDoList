﻿using System.Collections;
using ToDoList.Domain.Shared.ErrorManagement;

namespace ToDoList.Presentation.Response;

public class ResponseErrorList : IEnumerable<ResponseError>
{
    private readonly List<ResponseError> _responseErrors;

    private ResponseErrorList(IEnumerable<ResponseError> responseErrors)
    {
        _responseErrors = responseErrors.ToList();
    }

    public static ResponseErrorList FromErrorList(ErrorList? errorList)
    {
        if (errorList == null)
            return new ResponseErrorList([]);

        var responseErrors = errorList
            .Select(e => new ResponseError(e.Code, e.Message, e.InvalidPropertyName));
        return new ResponseErrorList(responseErrors);
    }

    public IEnumerator<ResponseError> GetEnumerator()
    {
        return _responseErrors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}