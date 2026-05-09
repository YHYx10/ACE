<template>
  <div class="team-list">
    <div class="header">
      <div class="nickname">{{ loc("captmenu_7") }}</div>
      <div class="rang">{{ loc("captmenu_8") }}</div>
    </div>
    <div class="team-list_list">
      <TeamListItem
        v-for="(item, index) in teamList"
        :key="index"
        :index="index"
        :item="item"
      />
    </div>
    <div class="btns-group">
      <div
        class="btn btn-apply"
        v-if="capturing.attackStatus === 'we'"
        @click="attack"
      >
        <span>{{ loc("captmenu_9") }}</span>
      </div>
      <div
        class="btn btn-apply"
        v-if="capturing.attackStatus === 'us'"
        @click="protect"
      >
        <span> {{ loc("captmenu_10") }}</span>
      </div>
      <div class="btn btn-cancel" @click="cancel">
        <span>{{ loc("captmenu_11") }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

import TeamListItem from "./TeamListItem";

export default {
  name: "TeamList",

  components: {
    TeamListItem,
  },

  computed: {
    ...mapState("captures", ["capturing", "teamList"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    attack: function () {
      window.mp.trigger("capt:attack");
    },
    protect: function () {
      window.mp.trigger("capt:deff");
    },
    cancel: function () {
      window.mp.trigger("capt:closeMenu");
    },
  },
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

.team-list {
  width: 100%;
  margin-top: conv(19, true);
  display: flex;
  flex-direction: column;

  .header {
    display: flex;
    align-items: center;
    padding-left: conv(40);
    margin-bottom: conv(19, true);

    div {
      font-family: "Akrobat";
      font-weight: 700;
      font-size: conv(14, true);
      line-height: conv(17, true);
      text-transform: uppercase;
      color: #ffffff;
    }

    .nickname {
      width: conv(198);
      margin-right: conv(45);
    }

    .rang {
      width: conv(150);
    }
  }

  &_list {
    padding-right: conv(15);
    overflow-y: scroll;
    min-height: conv(522, true);
    max-height: conv(522, true);
    height: conv(522, true);

    &::-webkit-scrollbar-track {
      border-radius: 0.25rem;
    }
    &::-webkit-scrollbar {
      width: 0.2rem;
    }
    &::-webkit-scrollbar-thumb {
      border-radius: 0.25rem;
      background: rgba(255, 255, 255, 0.15);
    }
  }

  .btns-group {
    display: grid;
    grid-template-columns: 1fr 1fr;
    column-gap: conv(20);
    margin-top: conv(40, true);
    width: 100%;

    .btn {
      width: 100%;
      height: conv(75, true);
      min-height: conv(75, true);
      display: flex;
      align-items: center;
      justify-content: center;
      font-weight: 700;
      font-size: conv(24, true);
      line-height: conv(29, true);
      text-transform: uppercase;
      color: #ffffff;
      position: relative;
      transition: all 0.2s ease;
      cursor: pointer;

      &::after {
        content: "";
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        transition: all 0.2s ease;
        z-index: 10;
        opacity: 0;
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 1) 0%,
          rgba(75, 0, 130, 1) 100%
        );
      }

      span {
        z-index: 15;
        font-family: "Akrobat";
      }

      &:hover {
        transition: all 0.2s ease;
      }
      &.btn-apply {
        color: rgba(255, 255, 255, 0.5);
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.3) 0%,
          rgba(75, 0, 130, 0.3) 100%
        );

        &:hover {
          color: #ffffff;
          &::after {
            opacity: 1;
          }
        }
      }
      &.btn-cancel {
        background: rgba(255, 255, 255, 0.05);

        &:hover {
          background: rgba(255, 255, 255, 0.15);
        }
      }
    }
  }
}
</style>