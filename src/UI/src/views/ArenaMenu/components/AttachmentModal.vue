<template>
  <div class="attachment-modal">
    <div class="attachment-modal__box">
      <div class="box__notify">
        <span v-if="!isAttachmentResult" class="notify__text">ID {{ room.id }}not found </span>
        <span v-else class="notify__text">{{ loc('arena_dm_17') }}</span>
      </div>
      <label class="box__input">
        <input
          v-focus
          v-model="roomId"
          type="text"
          placeholder="ID eingeben"
          maxlength="12"
        >
      </label>
      
      <div class="box__actions">
        <DefaultBtn class="btn" @click="searchLobby">
          <span>{{ loc('arena_dm_18') }}</span>
        </DefaultBtn>
        <DefaultBtn class="btn" @click="setIsAttachment(false)">
          <span>{{ loc('arena_dm_19') }}</span>
        </DefaultBtn>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations } from 'vuex'
import DefaultBtn from '../../UI/button/DefaultBtn.vue'

export default {
    name: "AttachmentModal",
    data: function () {
        return {
            roomId: ""
        };
    },
    computed: {
        ...mapState("arenaMenu", ["isAttachmentResult"]),
        ...mapGetters("localization", ["loc"])
    },
    methods: {
        ...mapMutations("arenaMenu", ["setIsAttachment"]),
        searchLobby: function () {
            // CALL EVENT
            window.mp.trigger("ARENA::JOIN::LOBBY::BY::ID::VUE", this.roomId);
            this.setIsAttachment(false);
            //console.log(this.roomId)
        }
    },
    directives: {
        focus: {
            inserted: function (el) {
                el.focus();
            }
        }
    },
    components: { DefaultBtn }
}
</script>

<style lang="scss" scoped>
.attachment-modal {
  position: fixed;
  width: 100vw;
  height: 100vh;
  text-transform: uppercase;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba($color: #000000, $alpha: .6);
  z-index: 9;
  display: flex;
  align-items: center;
  justify-content: center;
  animation: fade .4s;
  font-family: Akrobat;
  @keyframes fade {
    0% {
      opacity: 0;
    }
    100% {
      opacity: 1;
    }
  }
  &__box {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    gap: 1.5rem;
    width: 31rem;
    height: 14.8rem;
    box-shadow: inset 0rem 0rem 3.2rem rgba(71, 44, 132, 0.38);
    background: #050505;
    border: 0.1rem solid #5D3FD3;
    .box__input {
      width: 19rem;
      height: 2.2rem;
      background: linear-gradient(-90deg, rgba(255, 255, 255, 0.02) 0%, rgba(255, 255, 255, 0) 120% );
      border: 0.05rem solid;
      border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1;
      cursor: text;
      & input {
        color: #fff;
        width: 100%;
        height: 100%;
        background: transparent;
        outline: none;
        border: none;
        text-align: center;
        font-family: Akrobat;
        &::placeholder {
          text-transform: uppercase;
        }
      }
    }
    .box__notify {
      height: 3rem;
      color: #fff;
      display: flex;
      font-weight: 800;
      font-size: 1.2rem;
      line-height: 1.45rem;
      align-items: center;
      justify-content: center;
    }
    .box__actions {
      width: 100%;
      display: flex;
      justify-content: center;
      gap: 2rem;
      .btn {
        width: 6rem;
        height: 2.15rem;
        color: #fff;
        font-weight: 700;
        font-size: 0.7rem;
        line-height: 0.85rem;
      }
    }
  }
}
</style>
