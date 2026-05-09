<template>
  <div
    :class="[
      { attack: capturing.attackStatus === 'we' },
      { protect: capturing.attackStatus === 'us' },
      'captures',
    ]"
  >
    <img class="captures-gun" src="/img/captures/gun.png" alt="" />
    <div
      :class="{
        attack: capturing.attackStatus === 'we',
        protect: capturing.attackStatus === 'us',
      }"
      class="captures-body"
    >
      <div class="captures-group-container">
        <div class="header">
          <div class="title">{{ loc("captmenu_1") }}</div>
          <div class="gang-name">{{ capturing.myGangName }}</div>
        </div>
        <div class="desc">Invitation to capture</div>
        <GroupList />
      </div>
      <div class="captures-line"></div>
      <div class="captures-team-container">
        <div class="header">
          <div v-if="capturing.attackStatus === 'us'" class="title">
            {{ loc("captmenu_2") }}
          </div>
          <div v-else-if="capturing.attackStatus === 'we'" class="title">
            {{ loc("captmenu_3") }}
          </div>
          <div v-if="capturing.attackStatus === 'us'" class="gang-name">
            {{ `${capturing.enemy}` }}
          </div>
          <div v-else-if="capturing.enemy !== ''" class="gang-name">
            {{ `${loc("captmenu_4")} ${capturing.enemy}` }}
          </div>
          <div v-else class="gang-name">{{ loc("captmenu_5") }}</div>
        </div>
        <div class="desc">Composition of the coup</div>
        <TeamList />
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

import GroupList from "./components/GroupList";
import TeamList from "./components/TeamList";

export default {
  name: "Captures",

  data: function () {
    return {};
  },

  components: {
    GroupList,
    TeamList,
  },

  computed: {
    ...mapState("captures", ["capturing"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {},
};
</script>

<style lang="scss" scoped>
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / $height) * 100vh;
  } @else {
    @return ($px / $width) * 100vw;
  }
}

.captures {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.75);
  overflow: hidden;
  display: flex;
  align-items: center;
  justify-content: center;

  &,
  span,
  div,
  button {
    font-family: "Akrobat";
  }

  /* &::after {
    content: "";
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    border-radius: 50%;
    width: conv(1261, true);
    height: conv(1261, true);
    z-index: 1;
    background: rgba(255, 255, 255, 0.3);
    opacity: 0.25;
    filter: blur(400px);
  } */

  & > div,
  & > span,
  & > img {
    z-index: 5;
  }

  &-gun {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: conv(1389); //another 100% heihgt 1080
    height: auto;
    z-index: 3 !important;
    opacity: 0.15;
  }

  &-body {
    display: grid;
    grid-template-columns: 1fr auto 1fr;
    column-gap: conv(32);
    width: conv(1273);
    height: 100vh;
  }

  &-line {
    height: 100%;
    width: conv(2);
    background: linear-gradient(
      270deg,
      rgba(255, 255, 255, 0) 0%,
      rgba(255, 255, 255, 0.1) 50%,
      rgba(255, 255, 255, 0) 100%
    );
  }

  &-group-container,
  &-team-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
    height: 100vh;
    overflow: hidden;
    padding-top: conv(151, true);

    .header {
      display: flex;
      flex-direction: column;
      align-items: center;

      .title {
        font-weight: 700;
        font-size: conv(14, true);
        line-height: conv(17, true);
        text-transform: uppercase;
        color: rgba(255, 255, 255, 0.55);
      }

      .gang-name {
        font-weight: 700;
        font-size: conv(48, true);
        line-height: conv(58, true);
        text-transform: uppercase;
        color: #ffffff;
        margin-top: conv(-3, true);
        text-align: center;
      }
    }

    .desc {
      margin-top: conv(15.78, true);
      text-align: center;
      font-weight: 700;
      font-size: conv(16, true);
      line-height: conv(19, true);
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }
  }

  &-team-container{
    padding-bottom: conv(112, true);
  }

  &.attack {
    &:after {
      background-image: url("/img/captures/attack.png");
    }
  }
  &.protect {
    background-color: rgba(33, 15, 15, 0.9);
    .captures-group-container {
      background-color: rgba(0, 0, 0, 0.6);
      .header {
        &:after {
          content: "VS";
          color: #db4733;
          text-transform: uppercase;
          font-style: normal;
          font-weight: bold;
          font-size: 5rem;
          line-height: 5rem;
          text-shadow: 0 1rem 2rem rgba(219, 71, 51, 0.3);
          position: absolute;
          right: 0;
          bottom: 0;
          transform: translateX(50%);
        }
      }
    }
    &:after {
      background-image: url("/img/captures/protect.png");
    }
    &:before {
      background: #e1523f;
    }
  }
  /* .header {
    padding: 1.75rem 0 0 2rem;
    width: 100%;
    height: 9.2rem;
    min-height: 9.2rem;
    .title,
    .gang-name {
      text-transform: uppercase;
      font-style: normal;
      color: #ffffff;
    }
    .title {
      font-weight: 300;
      font-size: 1.5rem;
      line-height: 1.5rem;
      margin-bottom: 0.2rem;
    }
    .gang-name {
      font-weight: bold;
      font-size: 5rem;
      line-height: 5rem;
    }
  } */
  /* &-group-container {
    background-color: rgba(8, 40, 17, 0.7);
    width: 29.75rem;
    min-width: 29.75rem;
    height: 100%;
    overflow: hidden;
    position: relative;
    .header {
      margin-bottom: 0.75rem;
    }
  } */
}
</style>
