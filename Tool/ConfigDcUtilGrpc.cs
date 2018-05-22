// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: protocBuilder/configDcUtil.proto
#region Designer generated code

using System;
using System.Threading;
using System.Threading.Tasks;
using grpc = global::Grpc.Core;

namespace ECP.Util.ConfigDc.ProtoProxy {
  public static partial class ConfigDcUtil
  {
    static readonly string __ServiceName = "ECP.Util.ConfigDc.ProtoProxy.ConfigDcUtil";

    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest> __Marshaller_ServiceFindRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply> __Marshaller_ServiceFindReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest> __Marshaller_DbConfigRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply> __Marshaller_DbConfigReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest> __Marshaller_ApplicationConfigRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply> __Marshaller_ApplicationConfigReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest> __Marshaller_ServerAddressRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply> __Marshaller_ServerAddressReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply.Parser.ParseFrom);

    static readonly grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest, global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply> __Method_GetGrpcServiceConfig = new grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest, global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetGrpcServiceConfig",
        __Marshaller_ServiceFindRequest,
        __Marshaller_ServiceFindReply);

    static readonly grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest, global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply> __Method_GetDbConnectionConfig = new grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest, global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetDbConnectionConfig",
        __Marshaller_DbConfigRequest,
        __Marshaller_DbConfigReply);

    static readonly grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest, global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply> __Method_GetApplicationConfig = new grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest, global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetApplicationConfig",
        __Marshaller_ApplicationConfigRequest,
        __Marshaller_ApplicationConfigReply);

    static readonly grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest, global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply> __Method_GetServerAddress = new grpc::Method<global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest, global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetServerAddress",
        __Marshaller_ServerAddressRequest,
        __Marshaller_ServerAddressReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ECP.Util.ConfigDc.ProtoProxy.ConfigDcUtilReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ConfigDcUtil</summary>
    public abstract partial class ConfigDcUtilBase
    {
      public virtual global::System.Threading.Tasks.Task<global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply> GetGrpcServiceConfig(global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply> GetDbConnectionConfig(global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply> GetApplicationConfig(global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply> GetServerAddress(global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ConfigDcUtil</summary>
    public partial class ConfigDcUtilClient : grpc::ClientBase<ConfigDcUtilClient>
    {
      /// <summary>Creates a new client for ConfigDcUtil</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ConfigDcUtilClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ConfigDcUtil that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ConfigDcUtilClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ConfigDcUtilClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ConfigDcUtilClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply GetGrpcServiceConfig(global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetGrpcServiceConfig(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply GetGrpcServiceConfig(global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetGrpcServiceConfig, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply> GetGrpcServiceConfigAsync(global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetGrpcServiceConfigAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindReply> GetGrpcServiceConfigAsync(global::ECP.Util.ConfigDc.ProtoProxy.ServiceFindRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetGrpcServiceConfig, null, options, request);
      }
      public virtual global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply GetDbConnectionConfig(global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetDbConnectionConfig(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply GetDbConnectionConfig(global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetDbConnectionConfig, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply> GetDbConnectionConfigAsync(global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetDbConnectionConfigAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.DbConfigReply> GetDbConnectionConfigAsync(global::ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetDbConnectionConfig, null, options, request);
      }
      public virtual global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply GetApplicationConfig(global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetApplicationConfig(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply GetApplicationConfig(global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetApplicationConfig, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply> GetApplicationConfigAsync(global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetApplicationConfigAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigReply> GetApplicationConfigAsync(global::ECP.Util.ConfigDc.ProtoProxy.ApplicationConfigRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetApplicationConfig, null, options, request);
      }
      public virtual global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply GetServerAddress(global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetServerAddress(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply GetServerAddress(global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetServerAddress, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply> GetServerAddressAsync(global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest request, grpc::Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default(CancellationToken))
      {
        return GetServerAddressAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressReply> GetServerAddressAsync(global::ECP.Util.ConfigDc.ProtoProxy.ServerAddressRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetServerAddress, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ConfigDcUtilClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ConfigDcUtilClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ConfigDcUtilBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetGrpcServiceConfig, serviceImpl.GetGrpcServiceConfig)
          .AddMethod(__Method_GetDbConnectionConfig, serviceImpl.GetDbConnectionConfig)
          .AddMethod(__Method_GetApplicationConfig, serviceImpl.GetApplicationConfig)
          .AddMethod(__Method_GetServerAddress, serviceImpl.GetServerAddress).Build();
    }

  }
}
#endregion