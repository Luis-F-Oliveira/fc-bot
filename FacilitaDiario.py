from app.auth import Auth

class App(Auth):
    async def main() -> None:
        pass

if __name__ == "__main__":
    app = App()
    app.main()