<template>
  <div class="hudnew-left">
    <div class="hudnew-hints">
      <ul class="hudnew-hints__list">
        <li v-for="hint in hints" :key="hint.key" class="hudnew-hints__item">
          <img :src="hint.icon" :alt="hint.label" class="hud-icon hudnew-hints__icon" />
          <span class="hudnew-circle-key">{{ hint.key }}</span>
          <p>{{ hint.label }}</p>
        </li>
      </ul>
      <div class="hudnew-reports-placeholder"></div>
    </div>

    <div class="hudnew-map-row" :style="mapRowStyle">
      <div class="hudnew-status-stack">
        <div class="hudnew-status-item hudnew-status-item--voice" :class="{ active: mic }">
          <img class="hud-icon" :src="mic ? 'img/hudicons/mic-on.svg' : 'img/hudicons/mic-off.svg'" alt="Voice" />
          <div>
            <span>Voice</span>
            <b>{{ voiceModeLabel }}</b>
          </div>
        </div>

        <div class="hudnew-status-item">
          <img class="hud-icon" src="img/hudicons/hunger.svg" alt="Hunger" />
          <div>
            <span>Hunger</span>
            <b>{{ statusDisplays.hungerLevel }}%</b>
          </div>
        </div>

        <div class="hudnew-status-item">
          <img class="hud-icon" src="img/hudicons/thirst.svg" alt="Thirst" />
          <div>
            <span>Thirst</span>
            <b>{{ statusDisplays.thirstLevel }}%</b>
          </div>
        </div>

        <div class="hudnew-status-item" v-if="isGreenZone">
          <img class="hud-icon" src="img/hudicons/zone.svg" alt="Zone" />
          <div>
            <span>Zone</span>
            <b>Safe</b>
          </div>
        </div>
      </div>

      <div class="hudnew-location">
        <img src="img/hudicons/location.svg" alt="Location" class="hud-icon hudnew-location__icon" />
        <div class="hudnew-location__compass">N</div>
        <div class="hudnew-location__copy">
          <div class="hudnew-location__district">{{ map.title }}</div>
          <div class="hudnew-location__street">{{ map.descr }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  data() {
    return {
      hints: [
        { key: "K", label: "Phone", icon: "img/hudicons/phone.svg" },
        { key: "M", label: "Menu", icon: "img/hudicons/menu.svg" },
        { key: "I", label: "Inventory", icon: "img/hudicons/inventory.svg" },
        { key: "G", label: "Interaction", icon: "img/hudicons/interaction.svg" },
        { key: "F9", label: "Hide HUD", icon: "img/hudicons/hide-hud.svg" },
        { key: "`", label: "Cursor", icon: "img/hudicons/cursor.svg" },
      ],
    };
  },
  computed: {
    ...mapState("hud", [
      "mic",
      "voiceState",
      "map",
      "isGreenZone",
      "statusDisplays",
      "statusDisplayShow",
      "minimap",
      "bonusDonateMoney",
    ]),
    voiceModeLabel() {
      if (this.voiceState === 0) return this.mic ? "Mic On" : "Mic Off";
      return this.mic ? "Radio On" : "Radio Off";
    },
    mapRowStyle() {
      const left = (Number(this.minimap.leftX || 0) + Number(this.minimap.width || 0)) * 100;
      const bottom = (1 - Number(this.minimap.bottomY || 1)) * 100;

      return {
        left: `calc(${left}vw + 0.85rem)`,
        bottom: `calc(${bottom}vh + 1.25rem)`,
      };
    },
  },
};
</script>

<style lang="scss" scoped>
.hudnew-left {
  position: absolute;
  inset: 0;
  pointer-events: none;
  font-family: "HudGilroy", "Akrobat", sans-serif;
  color: #fff;
  text-transform: uppercase;
}

.hud-icon {
  display: block;
  min-width: 18px;
  min-height: 18px;
  opacity: 1;
  visibility: visible;
  flex: 0 0 auto;
}

.hudnew-hints {
  position: absolute;
  left: 2.4rem;
  top: 23.8rem;
  display: flex;
  align-items: flex-start;
  gap: 0.65rem;
}

.hudnew-hints__list {
  list-style: none;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column;
  gap: 0.38rem;
}

.hudnew-hints__item {
  display: flex;
  align-items: center;
  gap: 0.62rem;
  min-height: 1.62rem;
  opacity: 1;

  p {
    margin: 0;
    font-size: 12px;
    font-weight: 800;
    line-height: 1;
    letter-spacing: 0.04rem;
    color: rgba(255, 255, 255, 0.72);
    text-shadow: 0 0.1rem 0.34rem rgba(0, 0, 0, 0.6);
  }
}

.hudnew-hints__icon {
  width: 26px;
  height: 26px;
  object-fit: contain;
  filter: drop-shadow(0 2px 5px rgba(0, 0, 0, 0.75));
}

.hudnew-circle-key {
  min-width: 1.16rem;
  height: 1.16rem;
  padding: 0 0.2rem;
  border-radius: 50%;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  color: #fff;
  font-size: 0.62rem;
  font-weight: 900;
  line-height: 1;
  background: rgba(9, 10, 13, 0.58);
  border: 0.0625rem solid rgba(255, 255, 255, 0.23);
  box-shadow: 0 0 0.34rem rgba(0, 0, 0, 0.46);
}

.hudnew-reports-placeholder {
  width: 1.1rem;
  height: 1.1rem;
}

.hudnew-map-row {
  position: absolute;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: 0.78rem;
  max-width: 18.5rem;
  text-shadow: 0 0.12rem 0.34rem rgba(0, 0, 0, 0.72);
}

.hudnew-status-stack {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: 0.48rem;
}

.hudnew-location {
  display: flex;
  align-items: center;
  gap: 0.7rem;
  min-width: 17.2rem;
}

.hudnew-location__icon {
  width: 22px;
  height: 22px;
  object-fit: contain;
  filter: drop-shadow(0 2px 6px rgba(0, 0, 0, 0.78));
}

.hudnew-location__compass {
  width: 1.25rem;
  height: 1.25rem;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 0.76rem;
  font-weight: 900;
  color: #fff;
  background: rgba(9, 10, 13, 0.58);
  border: 0.0625rem solid rgba(255, 255, 255, 0.23);
}

.hudnew-location__district {
  font-size: 20px;
  font-weight: 900;
  line-height: 1;
  max-width: 14rem;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.hudnew-location__street {
  margin-top: 0.13rem;
  font-size: 12px;
  font-weight: 800;
  line-height: 1;
  color: rgba(255, 255, 255, 0.62);
  max-width: 14rem;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.hudnew-status-item {
  display: flex;
  align-items: center;
  gap: 0.56rem;
  white-space: nowrap;
  min-height: 1.46rem;

  img {
    width: 19px;
    height: 19px;
    object-fit: contain;
    filter:
      drop-shadow(0 0 0.16rem rgba(255, 255, 255, 0.14))
      drop-shadow(0 2px 6px rgba(0, 0, 0, 0.74));
    opacity: 1;
  }

  span,
  b {
    display: block;
    line-height: 1;
  }

  span {
    font-size: 10px;
    font-weight: 800;
    letter-spacing: 0.05rem;
    color: rgba(255, 255, 255, 0.46);
  }

  b {
    margin-top: 0.1rem;
    font-size: 12px;
    font-weight: 900;
    color: rgba(255, 255, 255, 0.86);
    text-shadow:
      0 0 0.18rem rgba(255, 255, 255, 0.16),
      0 0.12rem 0.34rem rgba(0, 0, 0, 0.72);
  }
}

.hudnew-status-item--voice {
  img {
    filter: drop-shadow(0 2px 6px rgba(0, 0, 0, 0.74));
    opacity: 1;
  }

  b {
    color: #c9c9c9;
  }

  &.active {
    img {
      opacity: 1;
      filter: drop-shadow(0 0 0.34rem rgba(45, 255, 120, 0.36));
    }

    b {
      color: #56ff93;
    }
  }
}
</style>




