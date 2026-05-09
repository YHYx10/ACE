<template>
  <div class="reports-tab">
    <div class="reports-tab__side">
      <div class="reports-tab__heading">
        <div class="category">section</div>
        <div class="title">Appeal to the administration</div>
      </div>
      <div class="reports-tab__side-main">
        <div class="reports-tab__heading">
          <div class="category">category</div>
          <div class="title">You can ask an interest in the server administration</div>
          <div class="subtitle">Short description for example</div>
        </div>
        <div class="reports-tab__side-main-img">
          <img src="img/optionsMenu/reportsTab/peedoor.png">
        </div>
        <div class="item__btn" v-if="reports.length > 0" @click="closeReport">Close the topic</div>
        <div class="item__btn" v-else>Contact the administration</div>
        <div class="raiting" v-if="reportRaiting" @mouseleave="raitOver=0">
          <div class="raiting-tittle">{{ loc("mmain:rep:rait:title") }}</div>
          <img
              v-for="(star, index) in stars"
              :key="index"
              src="/img/optionsMenu/reportsTab/star.svg"
              alt="star"
              class="raiting-star"
              :class="{'raiting-over': star <= raitOver}"
              @mouseenter="raitOver = star"
              @click="sendRaiting(star)"
          >
        </div>
      </div>
    </div>
    <div class="reports-tab__content">
      <div class="reports-tab__heading">
        <div class="category">section</div>
        <div class="title">Dialogue with the administrator</div>
      </div>
      <div class="reports-tab__content-main">
        <div class="messages" ref="list">
          <messages-item
              v-for="(item, index) in reports"
              :key="index"
              :item="item"
          />
        </div>
        <div class="input">
          <div class="input__body">
          <textarea
              v-if="!reportRaiting"
              v-model="messageText"
              class="input__main"
              placeholder="Send a message"
              maxlength="1000"
              @keydown.enter.exact.prevent="send"
              @keydown.enter.shift.exact.prevent="messageText += '\n'"
          >
        </textarea>
          </div>
          <button class="input__btn" @click="send"></button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapState, mapGetters} from 'vuex'
import MessagesItem from './common/MessagesItem'

export default {
  name: 'ReportsTab',

  components: {MessagesItem},

  data: function () {
    return {
      messageText: '',
      stars: [1, 2, 3, 4, 5],
      raitOver: 0
    }
  },
  methods: {
    send() {
      if (this.messageText.length < 3) return
      this.$store.commit('optionsMenu/sendReport', this.messageText);
      this.messageText = '';
      this.scrollBottom();
    },
    scrollBottom() {
      setTimeout(() => {
        const list = this.$refs.list;
        if (list) list.scrollTop = list.scrollHeight;
      }, 0)
    },
    closeReport() {
      this.$store.commit('optionsMenu/closeReport');
      this.messageText = '';
    },
    sendRaiting(rait) {
      this.$store.commit('optionsMenu/sendRaiting', rait);
      this.messageText = '';
    }
  },
  computed: {
    ...mapState('optionsMenu', ['reports', 'reportRaiting']),
    ...mapGetters('localization', ['loc'])
  },
  mounted() {
    this.scrollBottom()
  }
}
</script>

<style lang="scss" scoped>
.reports-tab {
  display: flex;
  justify-content: center;
  margin-top: 2.5rem;

  &__heading {
    margin: 0 0 1rem 0.7rem;
  }

  &__side {
    min-width: 23rem;
    max-width: 23rem;
    margin-right: 3rem;

    &-main {
      position: relative;
      display: flex;
      flex-flow: column nowrap;
      justify-content: space-between;
      border: 1px solid rgba(255,255,255,0.1);
      padding: 1rem 1.25rem 1.4rem 1.75rem;
      background: bottom right / cover no-repeat url("/img/optionsMenu/reportsTab/side_bg.png");
      margin-top: 1rem;

      .reports-tab__heading {
        margin: 0;

        .title {
          margin: 0.4rem 0;
          font-size: 1.4rem;
          line-height: 1.1;
        }

        .subtitle {
          font-size: 0.7rem;
          line-height: 0.8rem;
          margin-top: 0;
        }

        .category {
          font-size: 0.66rem;
          line-height: 0.77rem;
          &:before {
            left: -0.88rem;
          }
        }
      }

      &-img {
        width: 6.4rem;
        height: 7.3rem;
        margin: 1rem 0 2rem;
        & img {
          width: 100%;
          height: 100%;
          object-fit: contain;
        }
      }

      .item__btn {
        max-width: 15rem;
      }

      .raiting {
        position: absolute;
        text-align: center;
        bottom: -10.5rem;

        &-star {
          margin: 0 .5rem;
          opacity: .3;
          width: 2rem;
          height: 2rem;
        }

        &-over {
          opacity: 1;
        }

        &-tittle {
          color: #fff;
          font-size: 1.5rem;
          letter-spacing: .04rem;
          margin-bottom: 1rem;
        }
      }
    }
  }

  &__content {
    min-width: 50rem;
    max-width: 50rem;
    height: 42rem;
    display: flex;
    flex-flow: column nowrap;

    &-main {
      display: flex;
      flex-flow: column nowrap;
      height: 100%;
      width: 100%;
      overflow: auto;
      padding: 1.5rem;
      border: 1px solid rgba(255, 255, 255, 0.1);
      background: #000 linear-gradient(180deg, rgba(255, 255, 255, 0) 80%, rgba(255, 255, 255, 0.05) 100%);

      .messages {
        height: 100%;
        padding: 0 0.75rem 0 0;
        overflow-y: scroll;
        margin: 0 0 2rem 0;

        scrollbar-width: thin;
        scrollbar-color: #5cff80 #444444;

        &::-webkit-scrollbar {
          display: block;
          width: 0.1rem;
          height: 0;
        }

        &::-webkit-scrollbar-track {
          background: #444444;
        }

        &::-webkit-scrollbar-thumb {
          background-color: #5cff80;
        }
      }

      .input {
        display: flex;
        align-items: center;
        justify-content: space-between;
        flex-wrap: nowrap;

        &__body {
          width: 100%;
          height: 4rem;
          background: none;
          border: 1px solid rgba(255, 255, 255, 0.1);
          padding: 0.5rem;
        }

        &__main {
          width: 100%;
          height: 100%;
          font-weight: 600;
          padding: 1rem 4rem 1rem 1rem;
          background: none;
          border: none;
          color: #fff;
          font-size: 1rem;
          line-height: 1;
          font-family: inherit;
          resize: none;
          scrollbar-width: thin;
          scrollbar-color: #ababab transparent;
          text-transform: none !important;
          &::placeholder {
            text-transform: uppercase !important;
          }

          &::-webkit-scrollbar {
            width: 4px;
          }

          &::-webkit-scrollbar-track {
            background: none;
          }

          &::-webkit-scrollbar-thumb {
            background-color: #ababab;
            border-radius: 1rem;
          }

          &:focus {
            outline: none !important;
          }
        }

        &__btn {
          max-width: 4rem;
          min-width: 4rem;
          height: 4rem;
          margin-left: 0.5rem;
          background: #00FF38;
          display: flex;
          align-items: center;
          justify-content: center;

          &:after {
            content: "";
            width: 1rem;
            height: 1.33rem;
            background: center / contain no-repeat url("/img/optionsMenu/reportsTab/send_icon.svg");
            display: block;
            transition: 0.3s ease;
          }

          &:hover:after {
            transform: scale(1.15);
          }
        }
      }
    }
  }
}
</style>
