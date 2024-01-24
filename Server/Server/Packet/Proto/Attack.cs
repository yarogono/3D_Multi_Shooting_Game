// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Attack.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Protobuf.Protocol {

  /// <summary>Holder for reflection information generated from Attack.proto</summary>
  public static partial class AttackReflection {

    #region Descriptor
    /// <summary>File descriptor for Attack.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AttackReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxBdHRhY2sucHJvdG8SCFByb3RvY29sGg5Qcm90b2NvbC5wcm90byJICg1D",
            "X01lbGVlQXR0YWNrEhYKDmF0dGFja1BsYXllcklkGAEgASgFEh8KB3Bvc0lu",
            "Zm8YAiABKAsyDi5Qcm90b2NvbC5WZWMzIicKDVNfTWVsZWVBdHRhY2sSFgoO",
            "YXR0YWNrUGxheWVySWQYASABKAUidwoNQ19EYW1hZ2VNZWxlZRIWCg50YXJn",
            "ZXRQbGF5ZXJJZBgBIAEoBRIOCgZkYW1hZ2UYAiABKAUSJQoNdGFyZ2V0UG9z",
            "SW5mbxgDIAEoCzIOLlByb3RvY29sLlZlYzMSFwoPbWVsZWVJdGVtTnVtYmVy",
            "GAQgASgFIjcKDVNfRGFtYWdlTWVsZWUSFgoOdGFyZ2V0UGxheWVySWQYASAB",
            "KAUSDgoGZGFtYWdlGAIgASgFQhuqAhhHb29nbGUuUHJvdG9idWYuUHJvdG9j",
            "b2xiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.Protocol.ProtocolReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.Protocol.C_MeleeAttack), global::Google.Protobuf.Protocol.C_MeleeAttack.Parser, new[]{ "AttackPlayerId", "PosInfo" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.Protocol.S_MeleeAttack), global::Google.Protobuf.Protocol.S_MeleeAttack.Parser, new[]{ "AttackPlayerId" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.Protocol.C_DamageMelee), global::Google.Protobuf.Protocol.C_DamageMelee.Parser, new[]{ "TargetPlayerId", "Damage", "TargetPosInfo", "MeleeItemNumber" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.Protocol.S_DamageMelee), global::Google.Protobuf.Protocol.S_DamageMelee.Parser, new[]{ "TargetPlayerId", "Damage" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class C_MeleeAttack : pb::IMessage<C_MeleeAttack> {
    private static readonly pb::MessageParser<C_MeleeAttack> _parser = new pb::MessageParser<C_MeleeAttack>(() => new C_MeleeAttack());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<C_MeleeAttack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Protocol.AttackReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C_MeleeAttack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C_MeleeAttack(C_MeleeAttack other) : this() {
      attackPlayerId_ = other.attackPlayerId_;
      posInfo_ = other.posInfo_ != null ? other.posInfo_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C_MeleeAttack Clone() {
      return new C_MeleeAttack(this);
    }

    /// <summary>Field number for the "attackPlayerId" field.</summary>
    public const int AttackPlayerIdFieldNumber = 1;
    private int attackPlayerId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int AttackPlayerId {
      get { return attackPlayerId_; }
      set {
        attackPlayerId_ = value;
      }
    }

    /// <summary>Field number for the "posInfo" field.</summary>
    public const int PosInfoFieldNumber = 2;
    private global::Google.Protobuf.Protocol.Vec3 posInfo_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.Protocol.Vec3 PosInfo {
      get { return posInfo_; }
      set {
        posInfo_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as C_MeleeAttack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(C_MeleeAttack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AttackPlayerId != other.AttackPlayerId) return false;
      if (!object.Equals(PosInfo, other.PosInfo)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (AttackPlayerId != 0) hash ^= AttackPlayerId.GetHashCode();
      if (posInfo_ != null) hash ^= PosInfo.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (AttackPlayerId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(AttackPlayerId);
      }
      if (posInfo_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(PosInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (AttackPlayerId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(AttackPlayerId);
      }
      if (posInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PosInfo);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(C_MeleeAttack other) {
      if (other == null) {
        return;
      }
      if (other.AttackPlayerId != 0) {
        AttackPlayerId = other.AttackPlayerId;
      }
      if (other.posInfo_ != null) {
        if (posInfo_ == null) {
          PosInfo = new global::Google.Protobuf.Protocol.Vec3();
        }
        PosInfo.MergeFrom(other.PosInfo);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            AttackPlayerId = input.ReadInt32();
            break;
          }
          case 18: {
            if (posInfo_ == null) {
              PosInfo = new global::Google.Protobuf.Protocol.Vec3();
            }
            input.ReadMessage(PosInfo);
            break;
          }
        }
      }
    }

  }

  public sealed partial class S_MeleeAttack : pb::IMessage<S_MeleeAttack> {
    private static readonly pb::MessageParser<S_MeleeAttack> _parser = new pb::MessageParser<S_MeleeAttack>(() => new S_MeleeAttack());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<S_MeleeAttack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Protocol.AttackReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S_MeleeAttack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S_MeleeAttack(S_MeleeAttack other) : this() {
      attackPlayerId_ = other.attackPlayerId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S_MeleeAttack Clone() {
      return new S_MeleeAttack(this);
    }

    /// <summary>Field number for the "attackPlayerId" field.</summary>
    public const int AttackPlayerIdFieldNumber = 1;
    private int attackPlayerId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int AttackPlayerId {
      get { return attackPlayerId_; }
      set {
        attackPlayerId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as S_MeleeAttack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(S_MeleeAttack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AttackPlayerId != other.AttackPlayerId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (AttackPlayerId != 0) hash ^= AttackPlayerId.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (AttackPlayerId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(AttackPlayerId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (AttackPlayerId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(AttackPlayerId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(S_MeleeAttack other) {
      if (other == null) {
        return;
      }
      if (other.AttackPlayerId != 0) {
        AttackPlayerId = other.AttackPlayerId;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            AttackPlayerId = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class C_DamageMelee : pb::IMessage<C_DamageMelee> {
    private static readonly pb::MessageParser<C_DamageMelee> _parser = new pb::MessageParser<C_DamageMelee>(() => new C_DamageMelee());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<C_DamageMelee> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Protocol.AttackReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C_DamageMelee() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C_DamageMelee(C_DamageMelee other) : this() {
      targetPlayerId_ = other.targetPlayerId_;
      damage_ = other.damage_;
      targetPosInfo_ = other.targetPosInfo_ != null ? other.targetPosInfo_.Clone() : null;
      meleeItemNumber_ = other.meleeItemNumber_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public C_DamageMelee Clone() {
      return new C_DamageMelee(this);
    }

    /// <summary>Field number for the "targetPlayerId" field.</summary>
    public const int TargetPlayerIdFieldNumber = 1;
    private int targetPlayerId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int TargetPlayerId {
      get { return targetPlayerId_; }
      set {
        targetPlayerId_ = value;
      }
    }

    /// <summary>Field number for the "damage" field.</summary>
    public const int DamageFieldNumber = 2;
    private int damage_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Damage {
      get { return damage_; }
      set {
        damage_ = value;
      }
    }

    /// <summary>Field number for the "targetPosInfo" field.</summary>
    public const int TargetPosInfoFieldNumber = 3;
    private global::Google.Protobuf.Protocol.Vec3 targetPosInfo_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.Protocol.Vec3 TargetPosInfo {
      get { return targetPosInfo_; }
      set {
        targetPosInfo_ = value;
      }
    }

    /// <summary>Field number for the "meleeItemNumber" field.</summary>
    public const int MeleeItemNumberFieldNumber = 4;
    private int meleeItemNumber_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MeleeItemNumber {
      get { return meleeItemNumber_; }
      set {
        meleeItemNumber_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as C_DamageMelee);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(C_DamageMelee other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TargetPlayerId != other.TargetPlayerId) return false;
      if (Damage != other.Damage) return false;
      if (!object.Equals(TargetPosInfo, other.TargetPosInfo)) return false;
      if (MeleeItemNumber != other.MeleeItemNumber) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TargetPlayerId != 0) hash ^= TargetPlayerId.GetHashCode();
      if (Damage != 0) hash ^= Damage.GetHashCode();
      if (targetPosInfo_ != null) hash ^= TargetPosInfo.GetHashCode();
      if (MeleeItemNumber != 0) hash ^= MeleeItemNumber.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (TargetPlayerId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(TargetPlayerId);
      }
      if (Damage != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Damage);
      }
      if (targetPosInfo_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(TargetPosInfo);
      }
      if (MeleeItemNumber != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(MeleeItemNumber);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TargetPlayerId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(TargetPlayerId);
      }
      if (Damage != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Damage);
      }
      if (targetPosInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(TargetPosInfo);
      }
      if (MeleeItemNumber != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MeleeItemNumber);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(C_DamageMelee other) {
      if (other == null) {
        return;
      }
      if (other.TargetPlayerId != 0) {
        TargetPlayerId = other.TargetPlayerId;
      }
      if (other.Damage != 0) {
        Damage = other.Damage;
      }
      if (other.targetPosInfo_ != null) {
        if (targetPosInfo_ == null) {
          TargetPosInfo = new global::Google.Protobuf.Protocol.Vec3();
        }
        TargetPosInfo.MergeFrom(other.TargetPosInfo);
      }
      if (other.MeleeItemNumber != 0) {
        MeleeItemNumber = other.MeleeItemNumber;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            TargetPlayerId = input.ReadInt32();
            break;
          }
          case 16: {
            Damage = input.ReadInt32();
            break;
          }
          case 26: {
            if (targetPosInfo_ == null) {
              TargetPosInfo = new global::Google.Protobuf.Protocol.Vec3();
            }
            input.ReadMessage(TargetPosInfo);
            break;
          }
          case 32: {
            MeleeItemNumber = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class S_DamageMelee : pb::IMessage<S_DamageMelee> {
    private static readonly pb::MessageParser<S_DamageMelee> _parser = new pb::MessageParser<S_DamageMelee>(() => new S_DamageMelee());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<S_DamageMelee> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Protocol.AttackReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S_DamageMelee() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S_DamageMelee(S_DamageMelee other) : this() {
      targetPlayerId_ = other.targetPlayerId_;
      damage_ = other.damage_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public S_DamageMelee Clone() {
      return new S_DamageMelee(this);
    }

    /// <summary>Field number for the "targetPlayerId" field.</summary>
    public const int TargetPlayerIdFieldNumber = 1;
    private int targetPlayerId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int TargetPlayerId {
      get { return targetPlayerId_; }
      set {
        targetPlayerId_ = value;
      }
    }

    /// <summary>Field number for the "damage" field.</summary>
    public const int DamageFieldNumber = 2;
    private int damage_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Damage {
      get { return damage_; }
      set {
        damage_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as S_DamageMelee);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(S_DamageMelee other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TargetPlayerId != other.TargetPlayerId) return false;
      if (Damage != other.Damage) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TargetPlayerId != 0) hash ^= TargetPlayerId.GetHashCode();
      if (Damage != 0) hash ^= Damage.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (TargetPlayerId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(TargetPlayerId);
      }
      if (Damage != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Damage);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TargetPlayerId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(TargetPlayerId);
      }
      if (Damage != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Damage);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(S_DamageMelee other) {
      if (other == null) {
        return;
      }
      if (other.TargetPlayerId != 0) {
        TargetPlayerId = other.TargetPlayerId;
      }
      if (other.Damage != 0) {
        Damage = other.Damage;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            TargetPlayerId = input.ReadInt32();
            break;
          }
          case 16: {
            Damage = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
