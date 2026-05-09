<template>
  <div class="hudnew-right">
    <div class="hudnew-topline">
      <div class="hudnew-meta-item">
        <img class="hud-icon" src="img/hudicons/online.svg" alt="Online" />
        <span>{{ online }}</span>
      </div>
      <div class="hudnew-meta-item">
        <img class="hud-icon" src="img/hudicons/id.svg" alt="ID" />
        <span>ID {{ id }}</span>
      </div>
      <div class="hudnew-meta-item">
        <img class="hud-icon" src="img/hudicons/time.svg" alt="Time" />
        <span>{{ time }}</span>
      </div>
      <div class="hudnew-meta-item">
        <img class="hud-icon" src="img/hudicons/date.svg" alt="Date" />
        <span>{{ date }}</span>
      </div>
    </div>

    <div class="hudnew-wanted-stars" aria-label="Wanted level">
      <span
        v-for="star in 5"
        :key="star"
        class="hudnew-wanted-star"
        :class="{ active: star <= normalizedWantedLevel }"
      >
        <img src="img/hudicons/wanted-stars.svg" alt="" />
        <span class="hudnew-wanted-star__fallback">★</span>
      </span>
    </div>

    <div class="hudnew-logo-line">
      <img class="hud-icon hudnew-logo-line__logo" src="img/hudicons/server-logo.svg" alt="Server logo" />
      <img class="hud-icon hudnew-logo-line__en" src="img/hudicons/en1.svg" alt="EN1" />
    </div>

    <div v-if="isAdmin" class="hudnew-admin">ADMIN</div>

    <div class="hudnew-money-stack">
      <div class="hudnew-money-line hudnew-money-line--cash">
        <img class="hud-icon" src="img/hudicons/cash.svg" alt="Cash" />
        <span>${{ formatNumber(money) }}</span>
      </div>
      <div class="hudnew-money-line hudnew-money-line--bank">
        <img class="hud-icon" src="img/hudicons/bank.svg" alt="Bank" />
        <span>${{ formatNumber(bank) }}</span>
      </div>
      <div v-if="ammo > 0" class="hudnew-ammo">{{ ammo }} / {{ mammo }}</div>
    </div>

    <div v-if="isAdmin" class="hudnew-report-count">
      <span>Reports</span>
      <b>{{ reportsInQueue || 0 }}</b>
      <em>F6</em>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  methods: {
    formatNumber(value) {
      return Number(value || 0).toLocaleString("de-DE");
    },
  },
  computed: {
    ...mapState("reportMenu", ["reportsInQueue"]),
    ...mapState("hud", [
      "wantedLevels",
      "time",
      "date",
      "id",
      "online",
      "money",
      "bank",
      "ammo",
      "mammo",
      "moneyChange",
      "isAdmin",
    ]),
    normalizedWantedLevel() {
      const level = Number(this.wantedLevels || 0);
      return Math.max(0, Math.min(5, Number.isFinite(level) ? level : 0));
    },
  },
};
</script>

<style lang="scss" scoped>
.hudnew-right {
  position: relative;
  width: 18.9rem;
  color: #fff;
  font-family: "HudGilroy", "Akrobat", sans-serif;
  text-transform: uppercase;
  pointer-events: none;
  text-align: right;
  text-shadow: 0 0.12rem 0.34rem rgba(0, 0, 0, 0.68);
}

.hud-icon {
  display: block;
  min-width: 18px;
  min-height: 18px;
  opacity: 1;
  visibility: visible;
  flex: 0 0 auto;
}

.hudnew-topline {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 0.58rem;
  margin-bottom: 0.34rem;
}

.hudnew-meta-item {
  display: inline-flex;
  align-items: center;
  gap: 0.42rem;
  height: 1.28rem;

  img {
    width: 22px;
    height: 22px;
    object-fit: contain;
    filter: drop-shadow(0 2px 6px rgba(0, 0, 0, 0.78));
  }

  span {
    font-size: 0.78rem;
    font-weight: 900;
    line-height: 1;
    letter-spacing: 0.025rem;
    color: rgba(255, 255, 255, 0.86);
  }
}

.hudnew-logo-line {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 0.52rem;
}

.hudnew-logo-line__logo {
  width: 138px;
  height: 86px;
  object-fit: contain;
  filter:
    drop-shadow(0 0 0.32rem rgba(255, 255, 255, 0.16))
    drop-shadow(0 4px 10px rgba(0, 0, 0, 0.84));
}

.hudnew-logo-line__en {
  width: 42px;
  height: 42px;
  object-fit: contain;
  filter:
    drop-shadow(0 0 0.26rem rgba(55, 246, 255, 0.2))
    drop-shadow(0 3px 8px rgba(0, 0, 0, 0.78));
}

.hudnew-admin {
  margin-top: 0.18rem;
  font-size: 0.6rem;
  font-weight: 900;
  line-height: 1;
  letter-spacing: 0.13rem;
  color: #e42127;
}

.hudnew-wanted-stars {
  margin: 0.1rem 0 0.44rem;
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 0.23rem;
}

.hudnew-wanted-star {
  width: 1.08rem;
  height: 1.08rem;
  display: block;
  flex: 0 0 auto;
  position: relative;
  align-items: center;
  justify-content: center;
  opacity: 1;
  visibility: visible;
  color: rgba(255, 255, 255, 0.46);
  text-shadow: 0 0.08rem 0.18rem rgba(0, 0, 0, 0.76);
  transition: background 0.18s ease, filter 0.18s ease, transform 0.18s ease;

  img {
    position: relative;
    z-index: 2;
    width: 100%;
    height: 100%;
    display: block;
    object-fit: contain;
    opacity: 0.46;
    visibility: visible;
    filter:
      grayscale(1)
      brightness(2.6)
      drop-shadow(0 0.08rem 0.18rem rgba(0, 0, 0, 0.76));
  }

  &.active {
    color: #38d8ff;
    text-shadow:
      0 0 0.2rem rgba(54, 220, 255, 0.95),
      0 0 0.46rem rgba(36, 139, 255, 0.72);
    transform: translateY(-0.03rem);

    img {
      opacity: 1;
      filter:
        brightness(0)
        saturate(100%)
        invert(69%)
        sepia(90%)
        saturate(1715%)
        hue-rotate(159deg)
        brightness(104%)
        contrast(105%)
        drop-shadow(0 0 0.2rem rgba(54, 220, 255, 0.95))
        drop-shadow(0 0 0.46rem rgba(36, 139, 255, 0.72));
    }
  }
}

.hudnew-wanted-star__fallback {
  position: absolute;
  inset: 0;
  z-index: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 0.94rem;
  font-weight: 900;
  line-height: 1;
}

.hudnew-money-stack {
  margin-top: 1.35rem;
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  gap: 0.5rem;
}

.hudnew-money-line {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 0.48rem;

  img {
    object-fit: contain;
    filter: drop-shadow(0 2px 6px rgba(0, 0, 0, 0.78));
    opacity: 1;
  }

  span {
    font-weight: 900;
    line-height: 1;
    color: #fff;
    letter-spacing: 0.025rem;
  }
}

.hudnew-money-line--cash {
  img {
    width: 24px;
    height: 24px;
  }

  span {
    font-size: 1.3rem;
  }
}

.hudnew-money-line--bank {
  img {
    width: 22px;
    height: 22px;
    opacity: 1;
  }

  span {
    font-size: 1rem;
    color: rgba(255, 255, 255, 0.82);
  }
}

.hudnew-ammo {
  margin-top: 0.16rem;
  font-size: 0.62rem;
  font-weight: 900;
  color: rgba(255, 255, 255, 0.72);
}

.hudnew-report-count {
  margin-top: 1.04rem;
  display: inline-grid;
  grid-template-columns: auto auto auto;
  gap: 0.3rem;
  align-items: baseline;
  justify-content: flex-end;
  color: rgba(255, 255, 255, 0.75);

  span,
  em {
    font-style: normal;
    font-size: 0.55rem;
    font-weight: 800;
    letter-spacing: 0.08rem;
  }

  b {
    color: #fff;
    font-size: 0.8rem;
    line-height: 1;
  }
}
</style>

<style lang="scss">
.questmsg {
  top: clamp(14.85rem, 26vh, 17rem);
  right: 2.125rem;
}
</style>
