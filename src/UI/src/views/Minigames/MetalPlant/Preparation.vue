<template>
  <div class="mplant-prepare">
    <div class="mplant-prepare_body">
      <div class="mplant-prepare_title">The process of cleaning the metal</div>
      <div class="mplant-prepare_sub">
   The more humiliate red substances, the more ruddy ingots get
      </div>

      <div class="mplant-prepare-info">
        <div class="mplant-prepare-info_row">
          <div class="mplant-prepare-info_block">
            <div class="mplant-prepare-info_title">
              {{ loc("mplant:prepare:invalid") }}
            </div>
            <div class="mplant-prepare-info_elements">
              <Element
                v-for="(element, index) in allInvalidElements"
                :key="index"
                :element="element"
                :red="true"
              />
            </div>
          </div>
          <div class="mplant-prepare-info_block">
            <div class="mplant-prepare-info_title">
              {{ loc("mplant:prepare:valid") }}
            </div>
            <div class="mplant-prepare-info_elements">
              <Element
                v-for="(element, index) in allValidElements"
                :key="index"
                :element="element"
              />
            </div>
          </div>
        </div>
        <div class="mplant-prepare-info_row">
          <div class="mplant-prepare-info_block">
            <div class="mplant-prepare-info_elements">
              <Element :element="tempUpElement" />
              <div class="mplant-prepare-info_title">
                {{ loc("mplant:prepare:tup") }}
              </div>
            </div>
          </div>
          <div class="mplant-prepare-info_block">
            <div class="mplant-prepare-info_elements">
              <Element :element="tempDownElement" :red="true" :temp="true"/>
              <div class="mplant-prepare-info_title">
                {{ loc("mplant:prepare:down") }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="bank-btn mplant-prepare_btn" @click="start">Begin</div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import Element from "./Element.vue";

export default {
  data() {
    return {
      timer: 5,
      interval: -1,
    };
  },
  computed: {
    ...mapGetters("gameMetalPlant", [
      "allValidElements",
      "allInvalidElements",
      "tempUpElement",
      "tempDownElement",
    ]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    tick() {
      this.timer--;
      if (this.timer < 1) {
        //clearInterval(this.interval);
        this.startGame();
        this.$store.commit("sounds/play", {
          name: `metalPlant_start`,
          volume: 0.5,
        });
      } else
        this.$store.commit("sounds/play", {
          name: `metalPlant_timer`,
          volume: 0.5,
        });
    },
    ...mapActions("gameMetalPlant", ["startGame"]),
    start() {
      this.startGame();
      this.$store.commit("sounds/play", {
        name: `metalPlant_start`,
        volume: 0.5,
      });
    },
  },
  components: {
    Element,
  },
  mounted() {
    //this.interval = setInterval(this.tick, 1000);
    //this.$store.commit("sounds/play", { name: `metalPlant_timer`, volume: 0.5});
  },
  beforeDestroy() {
    //clearInterval(this.interval)
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.mplant-prepare {
  z-index: 500;
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(7, 7, 7, 0.96);
  display: flex;
  align-items: center;
  justify-content: center;

  &_body {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  &_title {
    font-weight: 700;
    font-size: conv(64);
    line-height: conv(77);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: conv(24);
  }

  &_sub {
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    white-space: nowrap;
  }

  &-info {
    width: conv(642);
    height: conv(237);
    display: grid;
    grid-template-columns: 1fr 1fr;
    margin: conv(45) 0;

    &_row {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      width: 100%;
      height: 100%;
      background: rgba(255, 255, 255, 0.04);

      &:first-child {
        align-items: flex-start;
        padding-left: conv(50);

        .mplant-prepare-info_title {
          margin-bottom: conv(10);
        }

        .mplant-prepare-info_elements > div {
          width: conv(35);
          height: conv(35);

          &:not(:last-child) {
            margin-right: conv(10);
          }
        }

        .mplant-prepare-info_block {
          display: flex;
          flex-direction: column;

          &:first-child {
            margin-bottom: conv(30);
          }
        }
      }

      &:last-child {
        .mplant-prepare-info_elements {
          width: 100%;

          & > div:first-child {
            width: conv(59);
            height: conv(59);
            margin-right: conv(20);
          }

          & > div:last-child {
            max-width: conv(108);
          }
        }

        .mplant-prepare-info_block {
          display: flex;
          align-items: center;

          &:first-child {
            margin-bottom: conv(26);
          }
        }
      }
    }

    &_title {
      font-weight: 700;
      font-size: conv(16);
      line-height: conv(19);
      text-transform: uppercase;
      color: #ffffff;
    }

    &_elements {
      display: flex;
      align-items: center;
    }
  }

  &_btn {
    display: flex;
    align-items: center;
    justify-content: center;
    width: conv(350);
    height: conv(75);
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    cursor: pointer;
  }
}
</style>