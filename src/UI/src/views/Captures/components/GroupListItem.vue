<template>
  <div class="item" :class="{ waiting: item.status }">
    <div class="nickname">
      <span>{{ item.nickname }}</span>
    </div>
    <div class="rang">
      <span>{{ item.rang }}</span>
    </div>
    <div class="btn" v-if="item.status === false">
      <img
        src="/img/captures/invite.svg"
        alt=""
        @click="inviteMember(item.nickname)"
      />
    </div>
    <div class="preloader" v-if="item.status === true">
      <img src="/img/captures/preloader.svg" alt="" />
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "GroupListItem",

  computed: {
    ...mapState("captures", ["capturing", "gangList", "teamList"]),
  },

  props: {
    item: Object,
    index: Number,
  },

  methods: {
    inviteMember: function (value) {
      window.mp.trigger("capt:inviteMember", value);
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

.item {
  overflow: hidden;
  position: relative;
  height: conv(72, true);
  min-height: conv(72, true);
  width: 100%;
  display: grid;
  grid-template-columns: conv(198) conv(150) 1fr;
  column-gap: conv(45);
  padding: 0 conv(70) 0 conv(40);
  border: 1px solid rgba(255, 255, 255, 0.09);
  background: rgba(255, 255, 255, 0.05);

  &:not(:last-child) {
    margin-bottom: conv(3, true);
  }

  /* &::after,
  &::before {
    content: "";
    position: absolute;
    bottom: conv(-59, true);
    left: 50%;
    transform: translate(-50%, 100%);
    width: 100%;
    height: conv(52, true);
    filter: blur(89px);
    border-radius: 50%;
    z-index: 5;
    transition: opacity 0.3s linear;
  }

  &::after {
    background: rgba(255, 255, 255, 0.55);
    opacity: 1;
  } */

  &.waiting {
    background: rgba(255, 202, 66, 0.05);
  }

  /* &::before {
    background: rgba(92, 255, 128, 0.55);
    opacity: 0;
  } */

  &:hover {
/*     &::after {
      opacity: 0;
    }

    &::before {
      opacity: 1;
    } */
    .btn{
      img{
        display: flex;
      }
    }
  }

  div,
  button,
  span,
  img {
    z-index: 6;
  }

  .rang,
  .nickname {
    display: flex;
    align-items: center;
    font-weight: 700;
    font-size: conv(18, true);
    line-height: conv(22, true);
    text-transform: uppercase;
    color: #ffffff;
  }

  .nickname {
    position: relative;
    width: conv(198); //44%;
    /* margin-right: conv(45); */

    &:before {
      content: "";
      position: absolute;
      top: 50%;
      left: conv(-13);
      transform: translate(-100%, -50%);
      width: conv(8, true);
      height: conv(8, true);
      border-radius: 50%;
      background: #5cff80;
    }
  }

  .rang {
    width: conv(150); //37%;
  }

  .preloader,
  .btn {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: flex-end;
  }

  .btn {
    cursor: pointer;
    img {
      display: none;
      width: conv(22, true);
      height: conv(22, true);
    }
  }

  .preloader {
    img {
      width: conv(24, true);
      height: conv(24, true);
    }
  }
}
/* .protect {
  .item {
    &:nth-child(2n + 1) {
      background: linear-gradient(
        90deg,
        rgba(255, 255, 255, 0.1) 0%,
        rgba(255, 255, 255, 0) 100%
      );
      border-radius: 0;
    }
    .btn {
      background: rgba(255, 255, 255, 0.2);
    }
  }
} */
/* @keyframes rotating {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
} */
</style>