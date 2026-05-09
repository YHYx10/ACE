<template>
  <div class="hudnew-speed" :style="{ opacity: inVeh ? 1 : 0 }">
    <div class="hudnew-speed-footer">
      <div class="hudnew-speed-footer__item hudnew-speed-footer__item--fuel">
        <img class="hud-icon" src="img/hudicons/fuel.svg" alt="Fuel" />
        <span>{{ Math.round(fuelPercent) }}%</span>
      </div>
      <div class="hudnew-speed-footer__item hudnew-speed-footer__item--belt" :class="{ off: !belt }">
        <img class="hud-icon" src="img/hudicons/belt.svg" alt="Belt" />
      </div>
      <div class="hudnew-speed-footer__item hudnew-speed-footer__item--engine" :class="{ off: !engine }">
        <img class="hud-icon" src="img/hudicons/engine.svg" alt="Engine" />
        <span>{{ Math.round(engineHealthPercent) }}%</span>
      </div>
      <div class="hudnew-speed-footer__item hudnew-speed-footer__item--lock" :class="{ off: !doors }">
        <img class="hud-icon" src="img/hudicons/lock.svg" alt="Lock" />
      </div>
    </div>

    <div class="hudnew-speedometer">
      <svg class="hudnew-speedometer__svg" viewBox="-4 -10 275 214" preserveAspectRatio="xMaxYMin meet">
        <path
          class="hudnew-speedometer__base-arc"
          fill-rule="evenodd"
          stroke="rgb(255, 255, 255)"
          stroke-width="7"
          stroke-linecap="butt"
          stroke-linejoin="miter"
          opacity="0.28"
          fill="none"
          d="M60.340,185.660 C18.553,143.874 18.553,76.126 60.340,34.340 C102.126,-7.447 169.874,-7.447 211.660,34.340 C253.446,76.126 253.446,143.874 211.660,185.660"
        />
        <path
          class="hudnew-speedometer__speed-arc"
          fill-rule="evenodd"
          stroke="#37f6ff"
          stroke-width="12"
          stroke-linecap="butt"
          stroke-linejoin="miter"
          opacity="1"
          fill="none"
          d="M60.340,185.660 C18.553,143.874 18.553,76.126 60.340,34.340 C102.126,-7.447 169.874,-7.447 211.660,34.340 C253.446,76.126 253.446,143.874 211.660,185.660"
          :style="speedArcStyle"
        />
      </svg>

      <svg class="hudnew-speedometer__svg" viewBox="-4 -10 275 214" preserveAspectRatio="xMaxYMin meet">
        <path
          class="hudnew-speedometer__side-base"
          fill-rule="evenodd"
          stroke="rgb(255, 255, 255)"
          stroke-width="8"
          stroke-linecap="butt"
          stroke-linejoin="miter"
          opacity="0.46"
          fill="none"
          pathLength="100"
          d="M30,178 A126,126 0 0 1 30,42"
        />
        <path
          class="hudnew-speedometer__fuel-side-arc"
          fill-rule="evenodd"
          stroke="#FFD54A"
          stroke-width="12"
          stroke-linecap="butt"
          stroke-linejoin="miter"
          opacity="1"
          fill="none"
          pathLength="100"
          d="M30,178 A126,126 0 0 1 30,42"
          :style="fuelArcStyle"
        />
      </svg>

      <svg class="hudnew-speedometer__svg" viewBox="-4 -10 275 214" preserveAspectRatio="xMaxYMin meet">
        <path
          class="hudnew-speedometer__side-base"
          fill-rule="evenodd"
          stroke="rgb(255, 255, 255)"
          stroke-width="8"
          stroke-linecap="butt"
          stroke-linejoin="miter"
          opacity="0.4"
          fill="none"
          pathLength="100"
          d="M242,178 A126,126 0 0 0 242,42"
        />
        <path
          class="hudnew-speedometer__danger-arc"
          fill-rule="evenodd"
          stroke="#FF3B3B"
          stroke-width="12"
          stroke-linecap="butt"
          stroke-linejoin="miter"
          opacity="1"
          fill="none"
          pathLength="100"
          d="M242,178 A126,126 0 0 0 242,42"
          :style="engineHealthArcStyle"
        />
      </svg>

      <div class="hudnew-speed-text">
        <div class="hudnew-gears">
          <span v-for="gear in gears" :key="gear" :class="{ active: gear === currentGear }">{{ gear }}</span>
        </div>
        <div><span>{{ formattedSpeed }}</span></div>
        <p>KM/H</p>
      </div>
      <div class="hudnew-speed-scale">
        <span v-for="mark in speedScaleMarks" :key="mark" :style="speedScaleMarkStyle(mark)">{{ mark }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  data() {
    return {
      displayedSpeed: 0,
      animationFrameId: null,
      speedArcLength: 504.295,
      healthArcLength: 118.296,
      fuelArcLength: 208.907,
      sideArcLength: 100,
      visualMaxSpeed: 240,
      gears: [1, 2, 3, 4, 5, 6],
      speedScaleMarks: [0, 20, 40, 60, 80, 100, 120, 140, 160, 180],
    };
  },
  computed: {
    ...mapState("speedometer", [
      "inVeh",
      "curSpeed",
      "maxSpeed",
      "curFuel",
      "maxFuel",
      "belt",
      "engine",
      "doors",
      "health",
    ]),
    safeSpeed() {
      return Math.max(0, Number(this.curSpeed || 0));
    },
    displayedSpeedPercent() {
      return Math.max(0, Math.min(100, (this.displayedSpeed / this.visualMaxSpeed) * 100));
    },
    fuelPercent() {
      const max = Math.max(Number(this.maxFuel || 1), 1);
      const value = Math.max(0, Number(this.curFuel || 0));
      return Math.max(0, Math.min(100, (value / max) * 100));
    },
    vehicleHealth() {
      return Math.max(0, Math.min(100, Math.round(Number(this.health || 0))));
    },
    engineHealthPercent() {
      const health = Number(this.health);
      if (Number.isFinite(health) && health > 0) return Math.max(0, Math.min(100, Math.round(health)));
      return this.engine ? 100 : 0;
    },
    currentGear() {
      if (this.displayedSpeed < 2) return 1;
      return Math.max(1, Math.min(6, Math.ceil(this.displayedSpeed / 40)));
    },
    formattedSpeed() {
      return String(Math.round(Math.max(0, this.displayedSpeed))).padStart(3, "0");
    },
    speedArcStyle() {
      return this.arcStyle(this.speedArcLength, this.displayedSpeedPercent);
    },
    healthArcStyle() {
      return this.arcStyle(this.sideArcLength, this.vehicleHealth);
    },
    fuelArcStyle() {
      return this.arcStyle(this.sideArcLength, this.fuelPercent);
    },
    engineHealthArcStyle() {
      return this.arcStyle(this.sideArcLength, this.engineHealthPercent);
    },
  },
  methods: {
    arcStyle(length, percent) {
      const clamped = Math.max(0, Math.min(100, percent));
      return {
        strokeDasharray: `${length}, ${length}`,
        strokeDashoffset: Math.max(0, length * ((100 - clamped) / 100)),
      };
    },
    speedScaleMarkStyle(value) {
      const startAngle = 220;
      const endAngle = 0;
      const angle = startAngle + (value / 180) * (endAngle - startAngle);
      const radians = (angle * Math.PI) / 180;
      const centerX = 50;
      const centerY = 59;
      const radius = 42.5;
      const labelTilt = Math.max(-24, Math.min(24, 90 - angle));

      return {
        left: `${centerX + radius * Math.cos(radians)}%`,
        top: `${centerY - radius * Math.sin(radians)}%`,
        transform: `translate(-50%, -50%) rotate(${labelTilt}deg)`,
      };
    },
    animateSpeed() {
      const targetSpeed = this.safeSpeed;
      const difference = targetSpeed - this.displayedSpeed;

      if (Math.abs(difference) < 0.1) {
        this.displayedSpeed = targetSpeed;
        this.animationFrameId = null;
        return;
      }

      this.displayedSpeed += difference * 0.16;
      this.animationFrameId = requestAnimationFrame(() => this.animateSpeed());
    },
  },
  watch: {
    curSpeed() {
      if (!this.animationFrameId) this.animationFrameId = requestAnimationFrame(() => this.animateSpeed());
    },
  },
  mounted() {
    this.displayedSpeed = this.safeSpeed;
  },
  beforeDestroy() {
    if (this.animationFrameId) cancelAnimationFrame(this.animationFrameId);
  },
};
</script>

<style lang="scss" scoped>
.hudnew-speed {
  position: absolute;
  right: 0;
  bottom: 1.35rem;
  width: 18.2rem;
  height: 14.65rem;
  pointer-events: none;
  transition: opacity 0.18s ease;
  font-family: "HudGilroy", "Akrobat", sans-serif;
  color: #fff;
}

.hud-icon {
  display: block;
  min-width: 18px;
  min-height: 18px;
  opacity: 1;
  visibility: visible;
  flex: 0 0 auto;
}

.hudnew-speed-footer {
  position: absolute;
  left: 1.1rem;
  right: 1.1rem;
  bottom: 0;
  height: 2rem;
  display: grid;
  grid-template-columns: 4.15rem 2.25rem 2.25rem 4.15rem;
  align-items: center;
  justify-content: space-between;
  column-gap: 0.52rem;
  z-index: 2;
}

.hudnew-speed-footer__item {
  opacity: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.38rem;
  min-width: 0;
  position: relative;

  img {
    width: 25px;
    height: 25px;
    object-fit: contain;
    filter:
      drop-shadow(0 0 0.18rem rgba(55, 246, 255, 0.52))
      drop-shadow(0 2px 6px rgba(0, 0, 0, 0.82));
  }

  span {
    font-size: 0.72rem;
    font-weight: 900;
    color: rgba(255, 255, 255, 0.9);
    line-height: 1;
    text-shadow: 0 0 0.28rem rgba(255, 232, 19, 0.36), 0 0.12rem 0.35rem rgba(0, 0, 0, 0.75);
  }

  &.off {
    opacity: 0.58;
  }
}

.hudnew-speed-footer__item--fuel {
  order: 1;
  min-width: 3.4rem;

  img {
    filter:
      drop-shadow(0 0 0.22rem rgba(255, 232, 19, 0.58))
      drop-shadow(0 2px 6px rgba(0, 0, 0, 0.82));
  }
}

.hudnew-speed-footer__item--engine {
  order: 4;
  min-width: 3.4rem;

  img {
    filter:
      drop-shadow(0 0 0.22rem rgba(255, 59, 59, 0.62))
      drop-shadow(0 2px 6px rgba(0, 0, 0, 0.82));
  }

  span {
    text-shadow: 0 0 0.28rem rgba(255, 59, 59, 0.42), 0 0.12rem 0.35rem rgba(0, 0, 0, 0.75);
  }
}

.hudnew-speed-footer__item--belt {
  order: 2;
}

.hudnew-speed-footer__item--lock {
  order: 3;
}

.hudnew-speedometer {
  position: absolute;
  right: 0;
  bottom: 0.95rem;
  width: 18.2rem;
  height: 14.2rem;
}

.hudnew-speedometer__svg {
  position: absolute;
  inset: 0;
  width: 18.2rem;
  height: 14.2rem;
  overflow: visible;
}

.hudnew-speedometer__speed-arc,
.hudnew-speedometer__fuel-side-arc,
.hudnew-speedometer__danger-arc {
  transition: stroke-dashoffset 0.14s linear;
}

.hudnew-speedometer__base-arc {
  filter: drop-shadow(0 0 0.18rem rgba(255, 255, 255, 0.16));
}

.hudnew-speedometer__speed-arc {
  filter:
    drop-shadow(0 0 0.22rem rgba(55, 246, 255, 0.92))
    drop-shadow(0 0 0.56rem rgba(55, 246, 255, 0.42));
}

.hudnew-speedometer__fuel-side-arc {
  filter:
    drop-shadow(0 0 0.16rem rgba(255, 213, 74, 0.8))
    drop-shadow(0 0 0.38rem rgba(255, 213, 74, 0.3));
}

.hudnew-speedometer__danger-arc {
  filter:
    drop-shadow(0 0 0.2rem rgba(255, 59, 59, 0.92))
    drop-shadow(0 0 0.48rem rgba(255, 59, 59, 0.36));
}

.hudnew-speedometer__side-base {
  opacity: 0.32;
  filter: drop-shadow(0 0 0.18rem rgba(255, 255, 255, 0.14));
}

.hudnew-speed-text {
  position: absolute;
  left: 50%;
  top: 54%;
  transform: translate(-50%, -50%);
  text-align: center;
  text-shadow: 0 0.16rem 0.45rem rgba(0, 0, 0, 0.74);
  z-index: 2;

  div {
    line-height: 0.82;
  }

  span {
    font-family: "HudHemi", "Akrobat", sans-serif;
    font-size: 2.95rem;
    font-weight: 900;
    letter-spacing: 0.035rem;
    color: #fff;
    text-shadow:
      0 0 0.28rem rgba(55, 246, 255, 0.32),
      0 0.2rem 0.55rem rgba(0, 0, 0, 0.86);
  }

  p {
    margin: 0.22rem 0 0;
    font-size: 0.58rem;
    font-weight: 900;
    letter-spacing: 0.13rem;
    color: rgba(255, 255, 255, 0.86);
    text-shadow: 0 0 0.24rem rgba(55, 246, 255, 0.28), 0 0.12rem 0.36rem rgba(0, 0, 0, 0.76);
  }
}

.hudnew-gears {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.38rem;
  margin-bottom: 0.42rem;

  span {
    font-family: "HudGilroy", "Akrobat", sans-serif;
    font-size: 0.7rem;
    font-weight: 900;
    line-height: 1;
    color: rgba(255, 255, 255, 0.48);
    text-shadow: 0 0 0.16rem rgba(255, 255, 255, 0.12), 0 0.1rem 0.28rem rgba(0, 0, 0, 0.76);

    &:not(:last-child)::after {
      content: "|";
      margin-left: 0.38rem;
      color: rgba(255, 255, 255, 0.3);
    }

    &.active {
      color: #fff;
      text-shadow:
        0 0 0.24rem rgba(55, 246, 255, 0.85),
        0 0 0.46rem rgba(55, 246, 255, 0.45),
        0 0.1rem 0.28rem rgba(0, 0, 0, 0.78);
    }
  }
}

.hudnew-speed-scale {
  position: absolute;
  inset: 0;
  z-index: 1;
}

.hudnew-speed-scale span {
  position: absolute;
  font-family: "HudGilroy", "Akrobat", sans-serif;
  font-size: 0.74rem;
  font-weight: 900;
  line-height: 1;
  color: rgba(255, 255, 255, 0.82);
  text-shadow:
    0 0 0.18rem rgba(255, 255, 255, 0.32),
    0 0 0.38rem rgba(55, 246, 255, 0.34),
    0 0.1rem 0.34rem rgba(0, 0, 0, 0.84);
}

</style>







