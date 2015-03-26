using System;
using UnityEngine;

namespace Holoville.HOTween.Plugins.Core
{
  /// <summary>
  /// Plugin for the "shaking" of Vector3 objects.
  /// </summary>
  public class PlugVector3Shake : ABSTweenPlugin
  {
    // VARS ///////////////////////////////////////////////////

    internal static Type[] validPropTypes = {typeof(Vector3)};
    internal static Type[] validValueTypes = {typeof(Vector3)};

    Vector3 typedStartVal;
    Vector3 typedEndVal;
    Vector3 changeVal;

    // GETS/SETS //////////////////////////////////////////////

    /// <summary>
    /// Gets the untyped start value,
    /// sets both the untyped and the typed start value.
    /// </summary>
    protected override object startVal
    {
      get
      {
        return _startVal;
      }
      set
      {
        if (tweenObj.isFrom && isRelative)
        {
          _startVal = typedStartVal = typedEndVal + (Vector3)value;
        }
        else
        {
          _startVal = typedStartVal = (Vector3)value;
        }
      }
    }

    /// <summary>
    /// Gets the untyped end value,
    /// sets both the untyped and the typed end value.
    /// </summary>
    protected override object endVal
    {
      get
      {
        return _endVal;
      }
      set
      {
        _endVal = typedEndVal = (Vector3)value;
      }
    }


    // ***********************************************************************************
    // CONSTRUCTOR
    // ***********************************************************************************

    /// <summary>
    /// Creates a new instance of this plugin using the main ease type.
    /// </summary>
    /// <param name="p_endVal">
    /// The <see cref="Vector3"/> value to tween to.
    /// </param>
    public PlugVector3Shake(Vector3 p_endVal)
      : base(p_endVal, false) {}

    /// <summary>
    /// Creates a new instance of this plugin.
    /// </summary>
    /// <param name="p_endVal">
    /// The <see cref="Vector3"/> value to tween to.
    /// </param>
    /// <param name="p_easeType">
    /// The <see cref="EaseType"/> to use.
    /// </param>
    public PlugVector3Shake(Vector3 p_endVal, EaseType p_easeType)
      : base(p_endVal, p_easeType, false) {}

    /// <summary>
    /// Creates a new instance of this plugin using the main ease type.
    /// </summary>
    /// <param name="p_endVal">
    /// The <see cref="Vector3"/> value to tween to.
    /// </param>
    /// <param name="p_isRelative">
    /// If <c>true</c>, the given end value is considered relative instead than absolute.
    /// </param>
    public PlugVector3Shake(Vector3 p_endVal, bool p_isRelative)
      : base(p_endVal, p_isRelative) {}

    /// <summary>
    /// Creates a new instance of this plugin.
    /// </summary>
    /// <param name="p_endVal">
    /// The <see cref="Vector3"/> value to tween to.
    /// </param>
    /// <param name="p_easeType">
    /// The <see cref="EaseType"/> to use.
    /// </param>
    /// <param name="p_isRelative">
    /// If <c>true</c>, the given end value is considered relative instead than absolute.
    /// </param>
    public PlugVector3Shake(Vector3 p_endVal, EaseType p_easeType, bool p_isRelative)
      : base(p_endVal, p_easeType, p_isRelative) {}

    /// <summary>
    /// Creates a new instance of this plugin.
    /// </summary>
    /// <param name="p_endVal">
    /// The <see cref="Vector3"/> value to tween to.
    /// </param>
    /// <param name="p_easeAnimCurve">
    /// The <see cref="AnimationCurve"/> to use for easing.
    /// </param>
    /// <param name="p_isRelative">
    /// If <c>true</c>, the given end value is considered relative instead than absolute.
    /// </param>
    public PlugVector3Shake(Vector3 p_endVal, AnimationCurve p_easeAnimCurve, bool p_isRelative)
      : base(p_endVal, p_easeAnimCurve, p_isRelative) {}

    // ===================================================================================
    // METHODS ---------------------------------------------------------------------------

    /// <summary>
    /// Returns the speed-based duration based on the given speed x second.
    /// </summary>
    protected override float GetSpeedBasedDuration(float p_speed)
    {
      float speedDur = changeVal.magnitude/p_speed;
      if (speedDur < 0)
      {
        speedDur = -speedDur;
      }
      return speedDur;
    }

    /// <summary>
    /// Sets the typed changeVal based on the current startVal and endVal.
    /// </summary>
    protected override void SetChangeVal()
    {
      if (isRelative && !tweenObj.isFrom)
      {
        changeVal = typedEndVal;
      }
      else
      {
        changeVal = new Vector3(typedEndVal.x - typedStartVal.x, typedEndVal.y - typedStartVal.y, typedEndVal.z - typedStartVal.z);
      }
    }

    /// <summary>
    /// Sets the correct values in case of Incremental loop type.
    /// </summary>
    /// <param name="p_diffIncr">
    /// The difference from the previous loop increment.
    /// </param>
    protected override void SetIncremental(int p_diffIncr)
    {
      typedStartVal += changeVal*p_diffIncr;
    }

    /// <summary>
    /// Updates the tween.
    /// </summary>
    /// <param name="p_totElapsed">
    /// The total elapsed time since startup.
    /// </param>
    protected override void DoUpdate(float p_totElapsed)
    {
      float time = 1f - ease(p_totElapsed, 0f, 1f, _duration, tweenObj.easeOvershootOrAmplitude, tweenObj.easePeriod);

      SetValue(new Vector3(
            typedStartVal.x + UnityEngine.Random.Range(-changeVal.x * time, changeVal.x * time),
            typedStartVal.y + UnityEngine.Random.Range(-changeVal.y * time, changeVal.y * time),
            typedStartVal.z + UnityEngine.Random.Range(-changeVal.z * time, changeVal.z * time)));
    }
  }
}